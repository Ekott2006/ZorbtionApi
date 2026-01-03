using Bogus;

namespace Core.Model.Faker;

public sealed class UserFaker : Faker<User>
{
    public UserFaker(string? userName = null)
    {
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.UserName, f => userName ?? f.Internet.UserName());
        RuleFor(c => c.ProfileImageUrl, _ => "https://avatar.iran.liara.run/public");
        RuleFor(u => u.UserStreaks, () => []);
        // RuleFor(u => u.UserStreaks, f =>
        //     f.Make(f.Random.Int(5, 20), () => DateOnly.FromDateTime(f.Date.RecentOffset(30).UtcDateTime))
        //         .Distinct()
        //         .OrderByDescending(x => x)
        //         .ToList());
        RuleFor(u => u.DeckOption, _ => new DeckOptionFaker());
        RuleFor(u => u.Decks, _ => []);
    }
}