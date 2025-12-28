namespace Core.Dto.Deck;

public record DeckSummaryResponse(int Id, string Name, string Description, DeckDueCountResponse DeckDueCount);