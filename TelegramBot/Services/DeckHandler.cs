using Core.Dto.Deck;
using Core.Dto.User;
using Core.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Helpers;

namespace TelegramBot.Services;

public class DeckHandler(IDeckService deckService, IUserService userService)
{
    public async Task HandleDashboard(ITelegramBotClient bot, long chatId, string telegramUserId, CancellationToken ct)
    {
        string? userId = await userService.GetByBotId(telegramUserId);
        if (userId is null)
        {
            await MessageHelper.SendError(bot, chatId, "You are not authenticated. Please use /auth <token> first.",
                ct);
            return;
        }

        UserDashboardResponse? dashboard = await userService.GetUserDashboard(userId);
        if (dashboard is null)
        {
            await MessageHelper.SendError(bot, chatId, "Failed to load dashboard data.", ct);
            return;
        }

        string reply = $"🔥 *Streak:* {dashboard.Streak} days\n" +
                       $"🧠 *Retention:* {dashboard.RetentionRate:P1}\n\n" +
                       $"📚 *Decks:* {dashboard.Decks.Count()}";

        foreach (UserDeckSummaryResponse deck in dashboard.Decks.Take(5))
        {
            DeckDueCountResponse due = deck.DeckDueCount;
            reply += $"\n- {deck.Name} (N:{due.New} L:{due.Learning} R:{due.Review})";
        }

        await bot.SendMessage(chatId, reply, ParseMode.Markdown, replyMarkup: KeyboardHelper.MainMenu,
            cancellationToken: ct);
    }

    public async Task HandleListDecks(ITelegramBotClient bot, long chatId, string telegramUserId, CancellationToken ct)
    {
        string? userId = await userService.GetByBotId(telegramUserId);
        if (userId is null)
        {
            await MessageHelper.SendError(bot, chatId, "You are not authenticated.", ct);
            return;
        }

        UserDashboardResponse? dashboard = await userService.GetUserDashboard(userId);
        if (dashboard is null || !dashboard.Decks.Any())
        {
            await MessageHelper.SendInfo(bot, chatId, "No decks found.", ct);
            return;
        }

        IEnumerable<(int Id, string Name)> decks = dashboard.Decks.Select(d => (d.Id, d.Name));
        await bot.SendMessage(chatId, "Select a deck to study:", replyMarkup: KeyboardHelper.DeckList(decks),
            cancellationToken: ct);
    }

    public async Task HandleDeckSelection(ITelegramBotClient bot, CallbackQuery query, string telegramUserId,
        int deckId, CancellationToken ct)
    {
        string? userId = await userService.GetByBotId(telegramUserId);
        if (userId is null) return;

        DeckSummaryResponse? summary = await deckService.GetSummary(userId, deckId);
        if (summary is null)
        {
            await MessageHelper.SendError(bot, query.Message!.Chat.Id, "Deck not found.", ct);
            return;
        }

        string text = $"📚 *{summary.Name}*\n{summary.Description}\n\n" +
                      $"🔵 New: {summary.DeckDueCount.New}\n" +
                      $"🔴 Learning: {summary.DeckDueCount.Learning}\n" +
                      $"🟢 Review: {summary.DeckDueCount.Review}";

        await bot.EditMessageText(
            query.Message!.Chat.Id,
            query.Message.MessageId,
            text,
            ParseMode.Markdown,
            KeyboardHelper.StudyOptions(deckId),
            cancellationToken: ct);
    }
}