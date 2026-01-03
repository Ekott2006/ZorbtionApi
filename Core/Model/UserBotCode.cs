namespace Core.Model;

// TODO: Use  Redis or any cache
public class UserBotCode
{
    public int Id { get; set; }
    public string RandomCode { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime ExpirationDate { get; set; }
}