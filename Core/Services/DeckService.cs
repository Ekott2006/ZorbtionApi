using Core.Data;
using Core.Dto.Deck;
using Core.Model;
using Core.Model.Helper;
using Core.Services.Helper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class DeckService(DataContext context, IFlashcardAlgorithmService flashcardAlgorithmService): IDeckService
{
    public async Task<Deck?> Create(string creatorId, CreateDeckRequest request)
    {
        Deck deck = new()
        {
            CreatorId = creatorId,
            Name = request.Name,
            Description = request.Description,
            Option = request.OptionRequest == null ?  null: (DeckOption)request.OptionRequest,
        };
        await context.Decks.AddAsync(deck);
        await context.SaveChangesAsync();
        return deck;
    }

    public async Task<int> Update(int id, string creatorId, UpdateDeckRequest request)
    {
        DeckOption? option = request.OptionRequest == null ? null : (DeckOption)request.OptionRequest;
        return await context.Decks.Where(x => x.Id == id && x.CreatorId == creatorId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(d => d.Name, request.Name)
                .SetProperty(d => d.Description, request.Description)
                .SetProperty(d => d.Option, option)
            );
    }

    public async Task<int> Delete(int id, string creatorId)
    {
        return await context.Decks
            .Where(x => x.CreatorId == creatorId && x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    
    public async Task<DeckStatisticsResponse?> GetStatistics(string creatorId, int id)
    {
        var deck = await context.Decks.Where(x => x.CreatorId == creatorId && x.Id == id)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                RawLearning = x.Cards.Count(c => c.State == CardState.Learning || c.State == CardState.Relearning),
                RawNew = x.Cards.Count(c => c.State == CardState.New),
                RawReview = x.Cards.Count(c => c.State == CardState.Review),
                RawReviewEaseFactor = x.Cards.Where(c => c.State == CardState.Review).Sum(card => card.EaseFactor),
                RawSuspended = x.Cards.Count(c => c.State == CardState.Suspended),
                x.DailyCounts
            }).FirstOrDefaultAsync();
        if (deck is null) return null;

        return new DeckStatisticsResponse(deck.Id, deck.Name, deck.Description,
            flashcardAlgorithmService.EstimateRetention(deck.RawReviewEaseFactor),
            new DeckCardCountsResponse(deck.RawLearning, deck.RawReview, deck.RawNew, deck.RawSuspended),
            deck.DailyCounts.Select(x => new DeckStatisticDailyCountResponse(x.Date, x.CardState, x.Count)).ToList()
        );
    }

    public async Task<DeckSummaryResponse?> GetSummary(string creatorId, int id)
    {
        DateTime today = DateTime.UtcNow.Date;

        var deck = await context.Decks
            .Where(x => x.CreatorId == creatorId && x.Id == id)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                LearningCount = x.Cards.Count(c => c.State == CardState.Learning || c.State == CardState.Relearning),
                RawNew = x.Cards.Count(c => c.State == CardState.New),
                RawReview = x.Cards.Count(c => c.State == CardState.Review && c.DueDate <= today),
                NewLimit = x.Option != null ? x.Option.NewLimitPerDay : x.Creator.DeckOption.NewLimitPerDay,
                ReviewLimit = x.Option != null ? x.Option.ReviewLimitPerDay : x.Creator.DeckOption.ReviewLimitPerDay
            })
            .FirstOrDefaultAsync();
        if (deck == null) return null;

        return new DeckSummaryResponse(
            deck.Id,
            deck.Name,
            deck.Description,
            new DeckDueCountResponse(
                deck.LearningCount,
                Math.Min(deck.RawReview, deck.ReviewLimit),
                Math.Min(deck.RawNew, deck.NewLimit)
            )
        );
    }
}