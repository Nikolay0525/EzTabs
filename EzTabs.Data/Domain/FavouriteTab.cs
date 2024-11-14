namespace EzTabs.Data.Domain;

public sealed class FavouriteTab
{
    public Guid UserId { get; set; }
    public Guid TabId { get; set; }
    public User? User { get; set; }
    public Tab? Tab { get; set; }
}
