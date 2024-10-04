namespace EzTabs.Model.Model.BaseClasses
{
    public class MessageBase : Entity
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string? Text { get; set; }
    }
}