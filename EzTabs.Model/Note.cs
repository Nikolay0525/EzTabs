using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Model
{
    public class Note
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int? Fret { get; set; }
        public string? String { get; set; }
        public Tab? Tab { get; set; }
    }
}
