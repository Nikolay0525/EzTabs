namespace EzTabs.Model
{
    public sealed class TabRate
    {
        public int Rate { get; set; }
        public Guid UserId { get; set; }
        public Guid TabId { get; set; }
        public User? User { get; set; }
        public Tab? Tab { get; set; }
    }
}
