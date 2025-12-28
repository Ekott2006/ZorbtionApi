namespace Core.Dto.User;

public record UserDashboardResponse(int Streak, double RetentionRate, IEnumerable<UserDeckSummaryResponse> Decks);