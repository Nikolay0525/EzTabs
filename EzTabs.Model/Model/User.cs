using EzTabs.Model.Model.BaseClasses;
using EzTabs.Model.Model.Enums;

namespace EzTabs.Model.Model
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
        public List<TabReport>? TabReports { get; set; }
        public List<TabRate>? TabRates { get; set; }
        public List<FavouriteTab>? FavouriteTabs { get; set; }
        public List<CommentReport>? CommentReports { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<CommentRate>? CommentRates { get; set; }
        public List<Tab>? Tab { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
