using EzTabs.Model.Model.BaseClasses;
using EzTabs.Model.Model.Enums;

namespace EzTabs.Model.Model
{
    public class Notification : MessageBase
    {
        public MessageType MessageType { get; set; }
    }
}
