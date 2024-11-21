using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.Services.ViewServices;

public static class ViewKeyReaderService
{
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
        if (sender is UIElement element)
        {
            var command = GetKeyCommand(element);

            if(Keyboard.Modifiers == ModifierKeys.Shift && e.Key >= Key.A && e.Key <= Key.Z)
            { 
                command.Execute($"{e.Key.ToString().ToUpper()}");
                return;               
            }
            else if(e.Key == Key.Oem5 || Keyboard.Modifiers == ModifierKeys.Shift && e.Key != Key.LeftShift & e.Key != Key.RightShift)
            {
                switch (e.Key)
                {
                    case Key.Oem5:
                        command.Execute("~");
                        break;
                    case Key.D0:
                        command.Execute(")");
                        break;
                    case Key.D1:
                        command.Execute("!");
                        break;
                    case Key.D2:
                        command.Execute("@");
                        break;
                    case Key.D3:
                        command.Execute("#");
                        break;
                    case Key.D4:
                        command.Execute("$");
                        break;
                    case Key.D5:
                        command.Execute("%");
                        break;
                    case Key.D6:
                        command.Execute("^");
                        break;
                    case Key.D7:
                        command.Execute("&");
                        break;
                    case Key.D8:
                        command.Execute("*");
                        break;
                    case Key.D9:
                        command.Execute("(");
                        break;
                    case Key.Oem2:
                        command.Execute("/");
                        break;
                    case Key.OemComma:
                        command.Execute("<");
                        break;
                    case Key.OemPeriod:
                        command.Execute(">");
                        break;
                    case Key.Oem3:
                        command.Execute("~");
                        break;
                    default:
                        break;
                }
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {

                command.Execute(e.Key.ToString().Replace("D",""));
            }
            else if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                command.Execute(e.Key.ToString().ToLower());
            }
        }
    }
}
