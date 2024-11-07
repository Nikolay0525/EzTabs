using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EzTabs.View.Window.MainControls.SimpleControls.ControlBarParts
{
    public partial class ControlBar : UserControl
    {
        public ControlBar()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlBar_Loaded);
        }

        void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {
            var w = System.Windows.Window.GetWindow(MenuButton);
            w.Deactivated += new EventHandler(ControlBarPopUpCloser_Loaded);
            if (w != null)
            {
                w.LocationChanged += delegate (object? sender2, EventArgs args)
                {
                    if (MenuPopup != null)
                    {
                        var offset = MenuPopup.HorizontalOffset;
                        MenuPopup.HorizontalOffset = offset + 1;
                        MenuPopup.HorizontalOffset = offset;
                    }
                };
                w.SizeChanged += delegate (object sender3, SizeChangedEventArgs e2)
                {
                    if (MenuPopup != null)
                    {
                        var offset = MenuPopup.HorizontalOffset;
                        MenuPopup.HorizontalOffset = offset + 1;
                        MenuPopup.HorizontalOffset = offset;
                    }
                };
            }
        }
        void ControlBarPopUpCloser_Loaded(object? sender, EventArgs e)
        {
            MenuPopup.IsOpen = false;
        }
    }
}
