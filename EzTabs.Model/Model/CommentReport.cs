using EzTabs.Model.Model.BaseClasses;

namespace EzTabs.Model.Model
{
    public class CommentReport : Dated
    {
        public string? Text { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
        public User? User { get; set; }
        public Comment? Comment { get; set; }
    }
}
