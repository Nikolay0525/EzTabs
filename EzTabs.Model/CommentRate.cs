namespace EzTabs.Model
{
    public sealed class CommentRate
    {
        public bool Rate { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
        public User? User { get; set; }
        public Comment? Comment { get; set; }
    }
}
