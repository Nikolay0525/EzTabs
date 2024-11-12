using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Services.WindowServices
{
    public interface IWindowService
    {
        bool SomethingLoading { get; set; }
        event Action? SomethingLoadingChanged;
    }
}
