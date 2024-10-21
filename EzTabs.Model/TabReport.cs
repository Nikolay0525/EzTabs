using EzTabs.Model.BaseModels;

namespace EzTabs.Model
{
    public sealed class TabReport
    {
        public string? Text { get; set; }
        public DateTime DateOfCreation { get; private set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public Guid TabId { get; set; }
        public User? User { get; set; }
        public Tab? Tab { get; set; }
    }
}
