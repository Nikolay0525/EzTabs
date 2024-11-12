using EzTabs.Services.NavigationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Services.WindowServices
{
    public class WindowService : IWindowService
    {
        private static WindowService? _instance;
        public static WindowService Instance => _instance ??= new WindowService();

        public event Action? SomethingLoadingChanged;

        private bool _somethingLoading = false;
        public bool SomethingLoading
        {
            get => _somethingLoading;
            set
            {
                _somethingLoading = value;
                SomethingLoadingChanged?.Invoke();
            }
        }
        private WindowService() { }
    }
}
