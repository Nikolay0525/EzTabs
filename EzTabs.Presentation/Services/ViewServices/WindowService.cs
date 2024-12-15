using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Presentation.Services.ViewServices
{
    public class WindowService : ObservableObject, IWindowService
    {
        private double _windowHeight;
        private double _windowWidth;

        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;
                OnPropertyChanged(nameof(WindowHeight));
            }
        }
        
        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;
                OnPropertyChanged(nameof(WindowWidth));
            }
        }

        public WindowService() { }

        public void ChangeHeightWidth(double windowHeight, double windowWidth)
        {
            WindowHeight = windowHeight;
            WindowWidth = windowWidth; 
        }

    }
}
