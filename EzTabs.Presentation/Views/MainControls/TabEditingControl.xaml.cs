using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EzTabs.Presentation.Views.MainControls;

public partial class TabEditingControl : UserControl
{
    public TabEditingControl()
    {
        InitializeComponent();
        this.Loaded += UserControl_Loaded;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        Keyboard.Focus(this); // Ensure the control receives keyboard input.
    }
}
