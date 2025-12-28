using Bogus;
using Core.Model.Helper;

namespace Core.Model.Faker;

public sealed class DeckOptionFaker : Faker<DeckOption>
{
    public DeckOptionFaker()
    {
        RuleFor(d => d.NewLimitPerDay, f => f.Random.Int(5, 30));
        RuleFor(d => d.ReviewLimitPerDay, f => f.Random.Int(20, 100));
        RuleFor(d => d.SortOrder, f => f.PickRandom<DeckOptionSortOrder>());
        RuleFor(d => d.InterdayLearningMix, f => f.Random.Bool());
    }
}