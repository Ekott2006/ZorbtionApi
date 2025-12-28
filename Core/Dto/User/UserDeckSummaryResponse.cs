using Core.Dto.Deck;

namespace Core.Dto.User;

public record UserDeckSummaryResponse(int Id, string Name, DeckDueCountResponse DeckDueCount);