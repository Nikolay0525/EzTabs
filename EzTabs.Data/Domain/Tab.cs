using EzTabs.Data.Domain.BaseModels;

namespace EzTabs.Data.Domain;

public sealed class Tab : Entity
{
    public Guid? AuthorId { get; set; }
    public string Title { get; set; } = "";
    public string Band { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Key { get; set; } = "";
    public int BitsPerMinute { get; set; } = 0;
    public string Description { get; set; } = "";
    public string TabText { get; set; } = "";
    public long Views { get; set; } = 0;
    public DateTime DateOfCreation { get; set; } = DateTime.Now;
    public User? Author { get; set; }
    public List<Tuning>? Tunings { get; set; }
    public List<TabReport>? TabReports { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<FavouriteTab>? FavouriteTabs { get; set; }
    public List<TabRate>? TabRates { get; set; }

    public void AddView()
    {
        Views++;
    }
}
