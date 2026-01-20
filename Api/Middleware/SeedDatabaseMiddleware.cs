using Bogus;
using Core.Data;
using Core.Model;
using Core.Model.Faker;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;

namespace Api.Middleware;

public static class SeedDatabaseMiddleware
{
    public static void UseSeedDatabaseMiddleware(this IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (context.Users.Count() > 3) return;

        SeedDatabase(context, userManager).Wait();
        if (!context.NoteTypeTemplates.Any() && !context.Decks.Any()) throw new Exception("Unable to Seed Database");
    }

    private static async Task SeedDatabase(DataContext context, UserManager<User> userManager)
    {
        const string password = "sv+4Fn6+VK2GU5W!";
        List<User> users = new UserFaker().GenerateBetween(2, 5);

        foreach (User user in users)
        {
            // 1. Create User (Standard Identity)
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) continue;

            // 2. Generate and Bulk Insert Parents (Decks and NoteTypes)
            var userDecks = new DeckFaker(user.Id).GenerateBetween(1, 2);
            var userNoteTypes = new NoteTypeFaker(user.Id).GenerateBetween(2, 4);

            // We MUST insert these now so the DB generates IDs for them
            // IncludeGraph ensures NoteTypeTemplates inside NoteTypes are also saved
            await context.BulkInsertAsync(userDecks);
            await context.BulkInsertAsync(userNoteTypes, b => b.IncludeGraph = true);

            // 3. Prepare Child Entities
            var allNotesForUser = new List<Note>();

            foreach (var deck in userDecks)
            {
                foreach (var noteType in userNoteTypes)
                {
                    var notes = new NoteFaker(deck.Id, 0, user.Id).GenerateBetween(2, 5);

                    foreach (var note in notes)
                    {
                        // Crucial: Set the FK IDs explicitly
                        note.DeckId = deck.Id;
                        note.NoteTypeId = noteType.Id;

                        // If you have cards, link them to the templates
                        foreach (var template in noteType.Templates)
                        {
                            var cards = new CardFaker(0, 0).GenerateBetween(1, 2);
                            foreach (var card in cards)
                            {
                                card.NoteTypeTemplateId = template.Id;
                                note.Cards.Add(card); // Cards will be saved via IncludeGraph
                            }
                        }
                    }
                    allNotesForUser.AddRange(notes);
                }
            }

            // 4. Finally, Bulk Insert the Notes (and their Cards)
            // Since Decks and NoteTypes are already in the DB, this will now succeed
            await context.BulkInsertAsync(allNotesForUser, b => b.IncludeGraph = true);
        }
    }
}
