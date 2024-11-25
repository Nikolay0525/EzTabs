using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.Views
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly IWindowService _windowService;

        public MainWindow(IWindowService windowService)
        {
            _windowService = windowService;
            InitializeComponent();

            _windowService = windowService;

            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _windowService.WindowWidth = e.NewSize.Width;
            _windowService.WindowHeight = e.NewSize.Height;
        }
    }
}