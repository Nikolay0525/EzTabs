using EzTabs.Presentation.Services.ViewModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EzTabs.Presentation.Services.ViewServices
{
    public static class FocusTrackingService
    {
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused",
                typeof(bool),
                typeof(FocusTrackingService),
                new PropertyMetadata(false, OnIsFocusedChanged));

        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if ((bool)e.NewValue)
                {
                    element.GotFocus += Element_GotFocus;
                    element.LostFocus += Element_LostFocus;
                }
                else
                {
                    element.GotFocus -= Element_GotFocus;
                    element.LostFocus -= Element_LostFocus;
                }
            }
        }

        private static void Element_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is ITrackElementFocus viewModel)
            {
                viewModel.IsFocused = true;
            }
        }

        private static void Element_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is ITrackElementFocus viewModel)
            {
                viewModel.IsFocused = false;
            }
        }
    }
}