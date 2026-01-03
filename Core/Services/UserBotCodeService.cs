using System.Security.Cryptography;
using Core.Data;
using Core.Model;
using Core.Model.Helper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserBotCodeService(DataContext context) : IUserBotCodeService
{
    public async Task<string> GenerateCode(string userId)
    {
        string code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        UserBotCode userBotCode = new()
        {
            RandomCode = code,
            UserId = userId,
            ExpirationDate = DateTime.UtcNow.AddMinutes(5)
        };
        await context.UserBotCodes.AddAsync(userBotCode);
        await context.SaveChangesAsync();
        return code;
    }


    public async Task<bool> VerifyCode(string code, string botId, UserBotType botType)
    {
        DateTime now = DateTime.UtcNow;

        UserBotCode? botCode = await context.UserBotCodes
            .Where(x => x.RandomCode == code && now <= x.ExpirationDate)
            .FirstOrDefaultAsync();
        if (botCode is null) return false;

        bool doesProviderExist =
            await context.UserBots.AnyAsync(x => x.BotId == botId && x.UserId == botCode.UserId);
        if (doesProviderExist) return true;

        UserBot userBot = new()
        {
            BotId = botId,
            UserId = botCode.UserId,
            Type = botType
        };
        context.UserBots.Add(userBot);
        await context.SaveChangesAsync();
        return true;
    }
}

public interface IUserBotCodeService
{
    Task<string> GenerateCode(string userId);
    Task<bool> VerifyCode(string code, string botId, UserBotType botType);
}