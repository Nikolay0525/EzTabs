namespace EzTabs.Data.Domain;

public sealed class TabRate
{
    private int _rate = 1;

    public int Rate
    {
        get => _rate;
        set
        {
            if (value < 1 || value > 5) return;
            _rate = value;
        }
    }
    public Guid UserId { get; set; }
    public Guid TabId { get; set; }
    public User? User { get; set; }
    public Tab? Tab { get; set; }
}
