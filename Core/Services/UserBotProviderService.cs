using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserBotProviderService(DataContext context) : IUserBotProviderService
{
    public async Task<int> Delete(string userId, string id)
    {
        return await context.UserBotProviders.Where(x => x.Id == id && x.UserId == userId).ExecuteDeleteAsync();
    }
}

public interface IUserBotProviderService
{
    Task<int> Delete(string userId, string id);
}