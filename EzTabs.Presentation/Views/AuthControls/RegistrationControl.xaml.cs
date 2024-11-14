using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.AuthControls;

public partial class RegistrationControl : UserControl
{
    public RegistrationControl()
    {
        InitializeComponent();

        var viewModel = new RegistrationControlViewModel();
        viewModel.ShowMessage += (title, message) =>
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        };

        DataContext = viewModel;
    }
}
