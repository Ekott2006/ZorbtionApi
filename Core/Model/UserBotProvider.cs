using Core.Model.Helper;

namespace Core.Model;

public class UserBotProvider
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public UserBotProviderType Type { get; set; }
}