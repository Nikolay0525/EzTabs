using EzTabs.Presentation.ViewModels;
using System.Windows;

namespace EzTabs.Presentation.Views
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainWindowViewModel();
            viewModel.ShowMessage += (title, message) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            };

            DataContext = viewModel;
        }
    }
}