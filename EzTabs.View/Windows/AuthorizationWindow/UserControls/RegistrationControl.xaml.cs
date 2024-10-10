using EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls;
using EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.View.Windows.AuthorizationWindow.UserControls
{
    /// <summary>
    /// Interaction logic for RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
            InitializeAsync();
        }

        public async void InitializeAsync()
        {
            var viewData = await Task.Run(() => new RegistrationControlViewModel(new Services.ModelServices.UserService()));
            DataContext = viewData;
            viewData.ShowMessage += (message, title) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }
    }
}
