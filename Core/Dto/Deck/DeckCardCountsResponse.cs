namespace Core.Dto.Deck;

public record DeckCardCountsResponse(int Learning, int Review, int New, int Suspended);