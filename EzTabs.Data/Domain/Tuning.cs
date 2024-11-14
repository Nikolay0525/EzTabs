namespace EzTabs.Data.Domain
{
    public sealed class Tuning
    {
        public int StringOrder { get; set; }
        public string? StringNote {  get; set; }
        public Guid? TabId { get; set; }
        public Tab? Tab { get; set; }
    }
}
