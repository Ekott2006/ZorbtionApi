using Core.Model.Helper;

namespace Core.Dto.Deck;

public record DeckStatisticDailyCountResponse(DateOnly Date, CardState Rating, int Count);