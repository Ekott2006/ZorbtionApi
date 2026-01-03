using Telegram.Bot;

namespace TelegramBot.Helpers;

public static class MessageHelper
{
    public static async Task SendError(ITelegramBotClient bot, long chatId, string message, CancellationToken ct)
    {
        await bot.SendMessage(chatId, $"❌ {message}", cancellationToken: ct);
    }

    public static async Task SendInfo(ITelegramBotClient bot, long chatId, string message, CancellationToken ct)
    {
        await bot.SendMessage(chatId, $"ℹ️ {message}", cancellationToken: ct);
    }

    public static async Task SendSuccess(ITelegramBotClient bot, long chatId, string message, CancellationToken ct)
    {
        await bot.SendMessage(chatId, $"✅ {message}", cancellationToken: ct);
    }
}