using EzTabs.Model;
using EzTabs.Model.Model;
using EzTabs.Model.Repository;
using EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls;
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
            var viewData = await Task.Run(() => new RegistrationControlViewModel(new RepoImplementation<User>(new EzTabsContext())));
            DataContext = viewData;
            viewData.ShowMessage += (message, title) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }
    }
}
