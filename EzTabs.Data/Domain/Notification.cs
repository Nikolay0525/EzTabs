using EzTabs.Data.Domain.BaseModels;
using EzTabs.Data.Domain.Enums;

namespace EzTabs.Data.Domain;

public sealed class Notification : MessageBase
{
    public MessageType Type { get; set; }
}
