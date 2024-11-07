using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EzTabs.View.Resources.Styles.CustomTypes
{
    public class ToggleImageButton : ToggleButton
    {
        public static readonly DependencyProperty PathDataProperty =
        DependencyProperty.Register(nameof(PathData), typeof(Geometry), typeof(ToggleImageButton), new PropertyMetadata(null));

        public Geometry PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }
    }
}
