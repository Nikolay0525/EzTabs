using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls
{
    public partial class MenuDropControl : UserControl
    {
        public MenuDropControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ListOfButtonsProperty =
        DependencyProperty.Register("ListOfButtons", typeof(List<ButtonInDropControl>), typeof(MenuDropControl), new PropertyMetadata(new List<ButtonInDropControl>()));

        public List<ButtonInDropControl> ListOfButtons
        {
            get { return (List<ButtonInDropControl>)GetValue(ListOfButtonsProperty); }
            set { SetValue(ListOfButtonsProperty, value); }
        }
    }
}
