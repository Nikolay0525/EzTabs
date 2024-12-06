using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls
{
    public partial class ComboBoxControl : UserControl
    {
        public ComboBoxControl()
        {
            InitializeComponent(); 
            this.Loaded += new RoutedEventHandler(ControlBar_Loaded);
        }

        void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(this);
            w.Deactivated += new EventHandler(ControlBarPopUpCloser_Loaded);
            if (w != null)
            {
                w.LocationChanged += (sender2, args) =>
                {
                    if (popup != null)
                    {
                        var popupOffset = popup.HorizontalOffset;
                        popup.HorizontalOffset = popupOffset + 1;
                        popup.HorizontalOffset = popupOffset;
                    }
                };
                w.SizeChanged += (sender3, e2) =>
                {
                    if (popup != null)
                    {
                        var popupOffset = popup.HorizontalOffset;
                        popup.HorizontalOffset = popupOffset + 1;
                        popup.HorizontalOffset = popupOffset;
                    }
                };
            }
        }
        void ControlBarPopUpCloser_Loaded(object? sender, EventArgs e)
        {
            popup.IsOpen = false;
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<ComboButtonControl>), typeof(ComboBoxControl), new PropertyMetadata(null));

        public ObservableCollection<ComboButtonControl> ItemsSource
        {
            get { return (ObservableCollection<ComboButtonControl>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ComboBoxControl), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ComboBoxControl), new PropertyMetadata(false));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }
    }
}
