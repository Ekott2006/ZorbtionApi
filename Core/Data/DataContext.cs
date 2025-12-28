using Core.Data.Helper;
using Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public class DataContext(DbContextOptions<DataContext> options): IdentityDbContext<User>(options)
{
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckDailyCount> DailyCounts { get; set; }
    public DbSet<NoteType> NoteTypes { get; set; }
    public DbSet<NoteTypeTemplate> NoteTypeTemplates { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<UserAiProvider> UserAiProviders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // TODO: Seed the Database(NoteTypes)
        
        builder.Entity<Note>().Property(b => b.Data).HasJsonConversion();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.AddInterceptors(
            new TimestampInterceptor()
        );
    }
}