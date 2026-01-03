using System.Security.Cryptography;
using Core.Data;
using Core.Dto.UserBot;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserBotCodeService(DataContext context) : IUserBotCodeService
{
    public async Task<string> GenerateAuthCode(string userId, UserBotAuthCodeRequest request)
    {
        string code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        UserBotCode userBotCode = new()
        {
            RandomCode = code,
            UserId = userId,
            ExpirationDate = DateTime.UtcNow.AddMinutes(5),
            Type = request.Type
        };
        await context.UserBotCodes.AddAsync(userBotCode);
        await context.SaveChangesAsync();
        return code;
    }


    public async Task<bool> VerifyCode(string code, string botId)
    {
        DateTime now = DateTime.UtcNow;

        UserBotCode? botCode = await context.UserBotCodes
            .Where(x => x.RandomCode == code && now <= x.ExpirationDate)
            .FirstOrDefaultAsync();
        if (botCode is null) return false;

        bool doesProviderExist =
            await context.UserBotProviders.AnyAsync(x => x.Id == botId && x.UserId == botCode.UserId);
        if (doesProviderExist) return true;

        UserBotProvider userBotProvider = new()
        {
            Id = botId,
            UserId = botCode.UserId,
            Type = botCode.Type
        };
        context.UserBotProviders.Add(userBotProvider);
        await context.SaveChangesAsync();
        return true;
    }
}

public interface IUserBotCodeService
{
    Task<string> GenerateAuthCode(string userId, UserBotAuthCodeRequest request);
    Task<bool> VerifyCode(string code, string botId);
}