﻿using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts;

public partial class ControlBar : UserControl
{
    public ControlBar()
    {
        InitializeComponent();
        this.Loaded += new RoutedEventHandler(ControlBar_Loaded);
    }

    void ControlBar_Loaded(object sender, RoutedEventArgs e)
    {
        var w = Window.GetWindow(MenuButton);
        w.Deactivated += new EventHandler(ControlBarPopUpCloser_Loaded);
        if (w != null)
        {
            w.LocationChanged += (sender2, args) =>
            {
                if (MenuPopup != null && NotificationPopup != null)
                {
                    var menuOffset = MenuPopup.HorizontalOffset;
                    MenuPopup.HorizontalOffset = menuOffset + 1;
                    MenuPopup.HorizontalOffset = menuOffset;

                    var notifOffset = NotificationPopup.HorizontalOffset;
                    NotificationPopup.HorizontalOffset = notifOffset + 1;
                    NotificationPopup.HorizontalOffset = notifOffset;
                }
            };
            w.SizeChanged += (sender3, e2) =>
            {
                if (MenuPopup != null && NotificationPopup != null)
                {
                    var menuOffset = MenuPopup.HorizontalOffset;
                    MenuPopup.HorizontalOffset = menuOffset + 1;
                    MenuPopup.HorizontalOffset = menuOffset;
                    
                    var notifOffset = NotificationPopup.HorizontalOffset;
                    NotificationPopup.HorizontalOffset = notifOffset + 1;
                    NotificationPopup.HorizontalOffset = notifOffset;
                }
            };
        }
    }
    void ControlBarPopUpCloser_Loaded(object? sender, EventArgs e)
    {
        MenuPopup.IsOpen = false;
        NotificationPopup.IsOpen = false;
    }

    public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register("Username", typeof(string), typeof(ControlBar), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty MenuButtonListProperty =
        DependencyProperty.Register("MenuButtonList", typeof(List<ButtonInDropControl>), typeof(ControlBar), new PropertyMetadata(new List<ButtonInDropControl>()));
    
    public string Username
    {
        get { return (string)GetValue(UsernameProperty); }
        set { SetValue(UsernameProperty, value); }
    }

    public List<ButtonInDropControl> MenuButtonList
    {
        get { return (List<ButtonInDropControl>)GetValue(MenuButtonListProperty); }
        set { SetValue(MenuButtonListProperty, value); }
    }
}
