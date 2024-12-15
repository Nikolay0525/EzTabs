namespace EzTabs.Data.Domain;

public sealed class CommentRate
{
    public Guid UserId { get; set; }
    public Guid CommentId { get; set; }
    public User? User { get; set; }
    public Comment? Comment { get; set; }
}
