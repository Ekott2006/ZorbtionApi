using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Helpers;

namespace TelegramBot.Services;

public class UpdateHandler(
    AuthHandler authHandler,
    DeckHandler deckHandler,
    StudyHandler studyHandler)
{
    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message when (update.Message?.Text is { } text):
                    await HandleMessage(bot, update.Message, text, ct);
                    break;
                case UpdateType.CallbackQuery when update.CallbackQuery is { } query:
                    await HandleCallback(bot, query, ct);
                    break;
            }
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error handling update: {ex}");
        }
    }

    private async Task HandleMessage(ITelegramBotClient botClient, Message message, string messageText,
        CancellationToken cancellationToken)
    {
        // Check if it's a reply to a study card
        if (message.ReplyToMessage?.Text != null && message.ReplyToMessage.Text.Contains("ID:"))
        {
            // Parse ID:CardId:DeckId
            string[] lines = message.ReplyToMessage.Text.Split('\n');
            string? idLine = lines.FirstOrDefault(l => l.Contains("ID:"));
            if (idLine != null)
            {
                // Extract ID:123:456 from the line (remove backticks if present)
                string cleanId = idLine.Replace("`", "").Trim();
                string[] parts = cleanId.Split(':');
                if (parts.Length == 3 && int.TryParse(parts[1], out int cardId) &&
                    int.TryParse(parts[2], out int deckId))
                {
                    await studyHandler.SubmitAnswer(botClient, message, messageText, cardId, deckId, cancellationToken);
                    return;
                }
            }
        }

        if (messageText.StartsWith("/auth"))
        {
            await authHandler.HandleAuth(botClient, message, cancellationToken);
            return;
        }

        if (messageText == "📊 Dashboard")
        {
            await deckHandler.HandleDashboard(botClient, message.Chat.Id, message.From!.Id.ToString(),
                cancellationToken);
            return;
        }

        if (messageText == "📚 Decks")
        {
            await deckHandler.HandleListDecks(botClient, message.Chat.Id, message.From!.Id.ToString(),
                cancellationToken);
            return;
        }

        // Default
        await botClient.SendMessage(message.Chat.Id, "Use the menu to navigate.", replyMarkup: KeyboardHelper.MainMenu,
            cancellationToken: cancellationToken);
    }

    private async Task HandleCallback(ITelegramBotClient bot, CallbackQuery query, CancellationToken ct)
    {
        string userId = query.From.Id.ToString();
        string data = query.Data ?? string.Empty;

        if (data.StartsWith("deck_"))
        {
            if (int.TryParse(data.Split('_')[1], out int deckId))
                await deckHandler.HandleDeckSelection(bot, query, userId, deckId, ct);
        }
        else if (data.StartsWith("study_"))
        {
            if (int.TryParse(data.Split('_')[1], out int deckId))
                await studyHandler.StartStudy(bot, query, userId, deckId, ct);
        }
        // Removed ShowAnswer and SubmitRating callbacks as they are no longer used.

        // Always answer callback to stop loading animation
        try
        {
            await bot.AnswerCallbackQuery(query.Id, cancellationToken: ct);
        }
        catch
        {
            // Ignore if already answered
        }
    }
}