using EzTabs.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Model
{
    public class Note : Entity
    {
        public int? Order { get; set; }
        public int? String { get; set; }
        public int? Fret { get; set; }
        public Tab? Tab { get; set; }
    }
}
