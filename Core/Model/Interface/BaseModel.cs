namespace Core.Model.Interface;

public abstract class BaseModel : IHasTimestamps
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}