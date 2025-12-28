using Bogus;
using Core.Model.Helper;

namespace Core.Model.Faker;

public sealed class DeckDailyCountFaker : Faker<DeckDailyCount>
{
    public DeckDailyCountFaker()
    {
        RuleFor(dc => dc.Date, f => DateOnly.FromDateTime(f.Date.Recent(7)));
        RuleFor(dc => dc.CardState, f => f.PickRandom<CardState>());
        RuleFor(dc => dc.Count, f => f.Random.Int(1, 100));
    }
}