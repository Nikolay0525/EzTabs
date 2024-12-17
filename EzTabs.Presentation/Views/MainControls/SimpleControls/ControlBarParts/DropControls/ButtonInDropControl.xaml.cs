using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls
{
    public partial class ButtonInDropControl : UserControl
    {
        public ButtonInDropControl()
        {
            Width = 170;
            InitializeComponent();
        }

        public static readonly DependencyProperty IsButtonVisibleProperty =
        DependencyProperty.Register("IsButtonVisible", typeof(bool), typeof(ButtonInDropControl), new PropertyMetadata(true));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ButtonInDropControl), new PropertyMetadata(string.Empty));
        
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(ButtonInDropControl), new PropertyMetadata(default));


        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }
    }
}
