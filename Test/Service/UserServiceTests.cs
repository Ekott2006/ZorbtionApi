using Core.Data;
using Core.Dto.Common;
using Core.Dto.User;
using Core.Model;
using Core.Services;
using Core.Services.Helper.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using Test.Helper;
using Xunit;

namespace Test.Service;

public class UserServiceFixture : DatabaseFixture
{
    protected override async Task SeedAsync(DataContext context)
    {
        User user = new() { Id = "test-user-class-seed", Email = "seed@class.com", UserName = "SeedUser" };
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}

public class UserServiceTests(UserServiceFixture fixture) : IntegrationTestBase<UserServiceFixture>(fixture)
{
    private UserService _userService;
    private readonly Mock<IFlashcardAlgorithmService> _mockAlgo = new();
    private readonly Mock<ITimeService> _mockTime = new();

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _userService = new UserService(Context, _mockAlgo.Object, _mockTime.Object);
    }

    [Fact]
    public async Task Get_SeededUser_ReturnsUser()
    {
        ResponseResult<UserResponse> result = await _userService.Get("test-user-class-seed");

        Assert.True(result.IsSuccess);
        Assert.Equal("SeedUser", result.Value.UserName);
    }

    [Fact]
    public async Task Create_TransactionRollback_Works()
    {
        // Add a user in this test
        User newUser = new() { Id = "temp-user", Email = "temp@test.com", UserName = "Temp" };
        Context.Users.Add(newUser);
        await Context.SaveChangesAsync();

        Assert.NotNull(await Context.Users.FindAsync("temp-user"));
    }

    [Fact]
    public async Task Check_TransactionRollback_Works_Part2()
    {
        // "temp-user" should NOT exist here if rollback works (assuming this runs after or independently)
        // With xUnit, order is undefined but isolation should hold regardless. 
        // If this runs BEFORE valid creation it passes. If it runs AFTER valid creation it passes (because of rollback).

        Assert.Null(await Context.Users.FindAsync("temp-user"));

        // Seeded user SHOULD exist
        Assert.NotNull(await Context.Users.FindAsync("test-user-class-seed"));
    }
}