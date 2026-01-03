using Core.Model.Helper;

namespace Core.Model;

public class UserBotCode
{
    public int Id { get; set; }
    public string RandomCode { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public UserBotProviderType Type { get; set; }
    public DateTime ExpirationDate { get; set; }
}