using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helpers;

public static class KeyboardHelper
{
    public static ReplyKeyboardMarkup MainMenu => new([["📊 Dashboard", "📚 Decks"]])
    {
        ResizeKeyboard = true
    };

    public static InlineKeyboardMarkup DeckList(IEnumerable<(int Id, string Name)> decks)
    {
        IEnumerable<InlineKeyboardButton[]> buttons = decks.Select(d => new[]
        {
            InlineKeyboardButton.WithCallbackData(d.Name, $"deck_{d.Id}")
        });
        return new InlineKeyboardMarkup(buttons);
    }

    public static InlineKeyboardMarkup StudyOptions(int deckId)
    {
        return new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("📖 Study Now", $"study_{deckId}")
        });
    }

    public static InlineKeyboardMarkup ShowAnswer(int deckId, int cardId)
    {
        return new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Show Answer", $"show_answer_{deckId}_{cardId}")
        });
    }

    public static InlineKeyboardMarkup RatingOptions(int deckId, int cardId)
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Again", $"rate_{deckId}_{cardId}_1"),
                InlineKeyboardButton.WithCallbackData("Hard", $"rate_{deckId}_{cardId}_2")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Good", $"rate_{deckId}_{cardId}_3"),
                InlineKeyboardButton.WithCallbackData("Easy", $"rate_{deckId}_{cardId}_4")
            }
        });
    }
}