using EzTabs.Model.Model.BaseClasses;

namespace EzTabs.Model.Model
{
    public class Tab : Entity
    {
        public Guid? AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Band { get; set; }
        public string? Genre { get; set; }
        public string? Key { get; set; }
        public int BitsPerMinute { get; set; } = 0;
        public string? Description { get; set; }
        public string? TabText { get; set; }
        public long Views { get; private set; } = 0;
        public User? Author { get; set; }
        public List<TabReport>? TabReports { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<FavouriteTab>? FavouriteTabs { get; set; }
        public List<TabRate>? TabRates { get; set; }

        public void AddView()
        {
            Views++;
        }
    }
}
