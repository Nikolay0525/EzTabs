using EzTabs.ViewModel.AuthControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.AuthControls
{
    /// <summary>
    /// Interaction logic for VerificationControl.xaml
    /// </summary>
    public partial class VerificationControl : UserControl
    {
        public VerificationControl()
        {
            InitializeComponent();
            var viewModel = new VerificationControlViewModel();
            viewModel.ShowMessage += (title, message) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            viewModel.ShowOkCancelMessage += (title, message) =>
            {
                MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                viewModel.UserConfirm = result == MessageBoxResult.OK;
            };
            DataContext = viewModel;
        }
    }
}
