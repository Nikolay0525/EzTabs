using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;

namespace EzTabs.Presentation.Services.ViewServices;

public static class ViewKeyReaderService
{
    private static Dictionary<Key, string> _stringProducedByKeyWithShift = new()
    {
        {Key.D0, ")"},
        {Key.D1, "!"},
        {Key.D2, "@"},
        {Key.D3, "#"},
        {Key.D4, "$"},
        {Key.D5, "%"},
        {Key.D6, "^"},
        {Key.D7, "&"},
        {Key.D8, "*"},
        {Key.D9, "("},
        {Key.Oem2, "/"},
        {Key.Oem3, "~"},
        {Key.OemComma, "<"},
        {Key.OemPeriod, ">"},
        {Key.OemMinus, "-"},
        {Key.OemPlus, "+"},
    };

    public static readonly DependencyProperty KeyCommandProperty =
        DependencyProperty.RegisterAttached("KeyCommand", typeof(ICommand), typeof(ViewKeyReaderService), new PropertyMetadata(null, OnKeyCommandChanged));

    public static ICommand GetKeyCommand(DependencyObject obj)
    {
        return (ICommand)obj.GetValue(KeyCommandProperty);
    }

    public static void SetKeyCommand(DependencyObject obj, ICommand value)
    {
        obj.SetValue(KeyCommandProperty, value);
    }

    private static void OnKeyCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            element.PreviewKeyDown -= Element_PreviewKeyDown;
            if (e.NewValue is ICommand)
            {
                element.PreviewKeyDown += Element_PreviewKeyDown;
            }
        }
    }

    private static void Element_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (IsTextInputFocused())
        {
            return;
        }

        if (sender is UIElement element)
        {
            var command = GetKeyCommand(element);

            if(Keyboard.Modifiers == ModifierKeys.Shift && e.Key >= Key.A && e.Key <= Key.Z)
            { 
                command.Execute($"{e.Key.ToString().ToUpper()}");
                return;               
            }
            if(Keyboard.Modifiers == ModifierKeys.Shift && e.Key != Key.LeftShift & e.Key != Key.RightShift)
            {
                command.Execute(_stringProducedByKeyWithShift.GetValueOrDefault(e.Key));
                return;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                command.Execute(e.Key.ToString().Replace("D",""));
                return;
            }
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                command.Execute(e.Key.ToString().ToLower());
                return;
            }
        }
    }
    private static bool IsTextInputFocused()
    {
        var focusedElement = Keyboard.FocusedElement;

        if (focusedElement is TextBoxBase)
        {
            return true;
        }

        return false;
    }
}
