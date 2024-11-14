using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls;

public partial class SearchControl : UserControl
{
    public SearchControl()
    {
        InitializeComponent();
        DataContext = new SearchControlViewModel();

    }
}
