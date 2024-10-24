using System.Windows.Controls;
using EzTabs.ViewModel.MainControlsViewModels;

namespace EzTabs.View.Window.MainControls
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();
            DataContext = new SearchControlViewModel();

        }
    }
}
