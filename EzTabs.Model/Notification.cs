using EzTabs.Model.BaseClasses;
using EzTabs.Model.Enums;

namespace EzTabs.Model
{
    public class Notification : MessageBase
    {
        public MessageType Type { get; set; }
    }
}
