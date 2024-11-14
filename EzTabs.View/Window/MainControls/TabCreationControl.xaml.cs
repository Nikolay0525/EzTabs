using EzTabs.ViewModel.MainControlsViewModels;
using System.Windows.Controls;
using System.Windows;

namespace EzTabs.Presentation.Views.MainControls
{
    /// <summary>
    /// Interaction logic for TabCreationControl.xaml
    /// </summary>
    public partial class TabCreationControl : UserControl
    {
        public TabCreationControl()
        {
            InitializeComponent();
            var viewModel = new TabCreationControlViewModel();
            viewModel.ShowMessage += (title, message) =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            };

            DataContext = viewModel;
        }
    }
}
