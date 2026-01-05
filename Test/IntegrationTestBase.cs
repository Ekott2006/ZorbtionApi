using Core.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Test.Helper;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Test;

public abstract class IntegrationTestBase<TFixture>(TFixture fixture) : IClassFixture<TFixture>, IAsyncLifetime
    where TFixture : DatabaseFixture
{
    protected readonly TFixture Fixture = fixture;
    protected DataContext Context { get; private set; }
    private IDbContextTransaction _transaction;

    public virtual async Task InitializeAsync()
    {
        Context = Fixture.CreateContext();
        _transaction = await Context.Database.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        
        if (Context != null)
        {
            await Context.DisposeAsync();
        }
    }
}
