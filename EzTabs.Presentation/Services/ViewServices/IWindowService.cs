using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Presentation.Services.ViewServices
{
    public interface IWindowService
    {
        double WindowHeight { get; set; }
        double WindowWidth { get; set; }
        void ChangeHeightWidth(double height,double width);
    }
}
