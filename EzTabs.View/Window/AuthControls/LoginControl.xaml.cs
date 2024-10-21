using EzTabs.ViewModel.AuthControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.View.Window.AuthControls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();

            var viewModel = new LoginControlViewModel();
            viewModel.ShowMessage += (title, message) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            };

            DataContext = viewModel;
        }
    }
}
