using Core.Data;
using Core.Dto.Deck;
using Core.Dto.User;
using Core.Model.Helper;
using Core.Services.Helper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

// Use a datetimeprovider for better testing
public class UserService(DataContext context, IFlashcardAlgorithmService flashcardAlgorithmService) : IUserService
{
    public async Task<int> UpdateProfileImage(string id, string profileImage) => await context.Users
        .Where(x => x.Id == id)
        .ExecuteUpdateAsync(x => x.SetProperty(y => y.ProfileImageUrl, profileImage));

    public async Task<int> Update(string id, UpdateUserRequest request) => await context.Users.Where(x => x.Id == id)
        .ExecuteUpdateAsync(x => x
            .SetProperty(u => u.DeckOption, request.DeckOption)
            .SetProperty(u => u.AiProviders, request.AiProviders)
        );

    public async Task<List<Dictionary<string, string>>?> GenerateFlashcards(string creatorId, int id, string description)
    {
        var provider = await context.UserAiProviders.FirstOrDefaultAsync(x => x.CreatorId == creatorId && x.Id == id);
        if (provider is null) return null;

        IAiService? aiService = AiServiceFactory.GetUserService(provider);
        if (aiService is null) return null;

        List<Dictionary<string, string>>? flashcards = await aiService.GenerateFlashcard(description);
        if (flashcards is null) return null;
        
        return flashcards;
    }

    // TODO: Check if you can combine the Context calls
    public async Task<UserDashboardResponse?> GetUserDashboard(string userId)
    {
        DateTime today = DateTime.UtcNow;

        // 1. Fetch User Streaks
        List<DateOnly>? streaks = await context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.UserStreaks)
            .FirstOrDefaultAsync();

        if (streaks == null) return null;

        // 2. Optimized Deck Query
        var deckData = await context.Decks
            .Where(x => x.CreatorId == userId)
            .Select(x => new
            {
                x.Id,
                x.Name,
                LearningCount = x.Cards.Count(c => c.State == CardState.Learning || c.State == CardState.Relearning),
                RawNew = x.Cards.Count(c => c.State == CardState.New),
                RawReview = x.Cards.Count(c => c.State == CardState.Review && c.DueDate <= today),
                RawReviewEaseFactor = x.Cards.Where(c => c.State == CardState.Review).Average(card => card.EaseFactor),
                NewLimit = x.Option != null ? x.Option.NewLimitPerDay : x.Creator.DeckOption.NewLimitPerDay,
                ReviewLimit = x.Option != null ? x.Option.ReviewLimitPerDay : x.Creator.DeckOption.ReviewLimitPerDay,
            })
            .ToListAsync();

        // 3. Project to DTOs
        IEnumerable<UserDeckSummaryResponse> deckResponses = deckData.Select(d => new UserDeckSummaryResponse(
            d.Id,
            d.Name,
            new DeckDueCountResponse(
                d.LearningCount,
                Math.Min(d.RawReview, d.ReviewLimit),
                Math.Min(d.RawNew, d.NewLimit)
            )
        ));
        // Use DefaultIfEmpty or a ternary check
        double retentionRate = deckData.Any()
            ? flashcardAlgorithmService.EstimateRetention(deckData.Select(x => x.RawReviewEaseFactor).Average())
            : 0.0;

        return new UserDashboardResponse(streaks.Count, retentionRate, deckResponses);
    }

    // TODO: Maybe store timezone for more accurate value
    public async Task UpdateStreakDaily()
    {
        DateOnly yesterdayDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));
        await context.Users
            .Where(x => !x.UserStreaks.Contains(yesterdayDate))
            .ExecuteUpdateAsync(x => x.SetProperty(u => u.UserStreaks, []));
    }
}