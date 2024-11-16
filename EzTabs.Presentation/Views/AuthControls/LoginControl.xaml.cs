using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.AuthControls;

public partial class LoginControl : UserControl
{
    public LoginControl()
    {
        InitializeComponent();

        /*var viewModel = new LoginControlViewModel();
        viewModel.ShowMessage += (title, message) =>
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        };

        DataContext = viewModel;*/
    }
}
