using Bogus;

namespace Core.Model.Faker;

public sealed class UserFaker : Faker<User>
{
    public UserFaker(string userName)
    {
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.UserName, _ => userName);
        RuleFor(c => c.ProfileImageUrl, _ => "https://avatar.iran.liara.run/public");
        RuleFor(u => u.UserStreaks, f =>
            f.Make(f.Random.Int(5, 30), () => DateOnly.FromDateTime(f.Date.Recent(30))).ToList());
        RuleFor(u => u.DeckOption, _ => new DeckOptionFaker());
        RuleFor(u => u.Decks, _ => new List<Deck>());
    }
}