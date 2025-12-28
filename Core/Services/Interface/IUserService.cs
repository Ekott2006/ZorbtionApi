using Core.Dto.User;

namespace Core.Services;

public interface IUserService
{
    Task<UserDashboardResponse?> GetUserDashboard(string userId);
    Task<int> UpdateProfileImage(string id, string profileImage);
    Task<int> Update(string id, UpdateUserRequest request);
}