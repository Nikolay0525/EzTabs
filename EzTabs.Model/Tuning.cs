using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Model
{
    public class Tuning
    {
        public int? StringOrder { get; set; }
        public string? StringNote {  get; set; }
        public Guid? TabId { get; set; }
        public Tab? Tab { get; set; }
    }
}
