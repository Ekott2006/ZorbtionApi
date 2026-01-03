using Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Test.Helper;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly IConfigurationRoot _config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables()
        .Build();

    private NpgsqlConnection? _connection;
    private IDbContextTransaction? _transaction;
    public DataContext? Context;

    public async Task InitializeAsync()
    {
        string connectionString = _config.GetConnectionString("DefaultConnection")
                                  ?? throw new InvalidOperationException("Connection string not found");

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
        DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(_connection)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;
        Context = new DataContext(options);
        await Context.Database.EnsureCreatedAsync();
        _transaction = await Context.Database.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        if (_transaction is null || _connection is null) return;
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}