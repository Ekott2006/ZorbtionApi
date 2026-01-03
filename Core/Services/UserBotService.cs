using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserBotService(DataContext context) : IUserBotService
{
    public async Task<string?> Get(string botId)
    {
        return await context.UserBots.Where(x => x.BotId == botId).Select(x => x.UserId).FirstOrDefaultAsync();
    }

    public async Task<int> Delete(string userId, int id)
    {
        return await context.UserBots.Where(x => x.Id == id && x.UserId == userId).ExecuteDeleteAsync();
    }
}

public interface IUserBotService
{
    Task<int> Delete(string userId, int id);
    Task<string?> Get(string botId);
}