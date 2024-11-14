using System.Windows.Controls;
using EzTabs.ViewModel.MainControlsViewModels;

namespace EzTabs.Presentation.Views.MainControls
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
