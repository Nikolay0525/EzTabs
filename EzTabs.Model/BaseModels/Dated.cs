using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Model.BaseModels
{
    public abstract class Dated
    {
        public DateTime DateOfCreation { get; private set; }
        public Dated()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
