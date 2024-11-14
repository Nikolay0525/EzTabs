using EzTabs.Data.Domain.BaseModels;
using EzTabs.Data.Domain.Enums;

namespace EzTabs.Data.Domain;

public sealed class Note : Entity
{
    public int Order { get; set; } = 0;
    public int String { get; set; } = 1;
    public NoteLengths Length { get; set; } = NoteLengths.Whole;
    public int Fret { get; set; } = 0;
    public bool IsSelected { get; set; } = false;
    public Tab? Tab { get; set; }
}
