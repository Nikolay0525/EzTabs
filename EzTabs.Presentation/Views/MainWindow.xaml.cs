using EzTabs.Data;
using EzTabs.Presentation.Services.ViewServices;
using System.Windows;

namespace EzTabs.Presentation.Views
{
    public partial class MainWindow : System.Windows.Window
    {
        private IWindowService _windowService;
        private readonly EzTabsContext _context;

        public MainWindow(IWindowService windowService, EzTabsContext context)
        {
            _context = context;
            _windowService = windowService;
            InitializeComponent();
            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _windowService.ChangeHeightWidth(e.NewSize.Height, e.NewSize.Width);
        }
    }
}