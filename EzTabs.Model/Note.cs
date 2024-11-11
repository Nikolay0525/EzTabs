using EzTabs.Model.BaseModels;
using EzTabs.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Model
{
    public sealed class Note : Entity
    {
        public int Order { get; set; } = 0;
        public int String { get; set; } = 1;
        public NoteLengths Length { get; set; } = NoteLengths.Whole;
        public int Fret { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        public Tab? Tab { get; set; }
    }
}
