using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls
{
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
