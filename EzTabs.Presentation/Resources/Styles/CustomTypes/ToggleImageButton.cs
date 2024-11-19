using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EzTabs.Presentation.Resources.Styles.CustomTypes;

public class ToggleImageButton : ToggleButton
{
    public static readonly DependencyProperty PathDataProperty =
    DependencyProperty.Register(nameof(PathData), typeof(Geometry), typeof(ToggleImageButton), new PropertyMetadata(null));


    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(ToggleImageButton), new PropertyMetadata(Stretch.Uniform));


    public Geometry PathData
    {
        get { return (Geometry)GetValue(PathDataProperty); }
        set { SetValue(PathDataProperty, value); }
    }

    public Stretch Stretch
    {
        get { return (Stretch)GetValue(StretchProperty); }
        set { SetValue(StretchProperty, value); }
    }
}
