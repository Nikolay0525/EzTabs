using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EzTabs.Presentation.Resources.Styles.CustomTypes;

public class ImageButton : Button
{
    public static readonly DependencyProperty PathDataProperty =
        DependencyProperty.Register(nameof(PathData), typeof(Geometry), typeof(ImageButton), new PropertyMetadata(null));

    public Geometry PathData
    {
        get { return (Geometry)GetValue(PathDataProperty); }
        set { SetValue(PathDataProperty, value); }
    }
}
