using EzTabs.Model.BaseClasses;

namespace EzTabs.Model
{
    public class Comment : MessageBase
    {
        public Guid TabId { get; set; }
        public Guid? ParentCommentId { get; set; } = null;
        public Tab? Tab { get; set; }
        public List<CommentReport>? CommentReports { get; set; }
        public List<CommentRate>? CommentRates { get; set; }
    }
}
