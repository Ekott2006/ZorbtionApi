namespace Core.Dto.Deck;

public record DeckStatisticsResponse(
    int Id,
    string Name,
    string Description,
    double RetentionRate,
    DeckCardCountsResponse DeckCardCounts,
    List<DeckStatisticDailyCountResponse> DailyCounts);