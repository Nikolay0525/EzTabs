using EzTabs.ViewModel.AuthControlsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EzTabs.View.Window.AuthControls
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
