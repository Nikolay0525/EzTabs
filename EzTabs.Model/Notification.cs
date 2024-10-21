using EzTabs.Model.BaseModels;
using EzTabs.Model.Enums;

namespace EzTabs.Model
{
    public sealed class Notification : MessageBase
    {
        public MessageType Type { get; set; }
    }
}
