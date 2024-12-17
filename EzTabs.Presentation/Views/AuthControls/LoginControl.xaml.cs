using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EzTabs.Presentation.Views.AuthControls;

public partial class LoginControl : UserControl
{
    private readonly List<char> _passwordBuffer = new();

    public LoginControl()
    {
        InitializeComponent();
    }
}
