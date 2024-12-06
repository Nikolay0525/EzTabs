namespace EzTabs.Data.Domain.BaseModels;

public abstract class MessageBase : Entity
{
    public Guid? UserId { get; set; }
    public string? Text { get; set; }
    public DateTime DateOfCreation { get; set; } = DateTime.Now;
    public User? User { get; set; }
}