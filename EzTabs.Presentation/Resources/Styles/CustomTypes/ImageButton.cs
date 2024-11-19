using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.Uniform));

    public Stretch Stretch
    {
        get { return (Stretch)GetValue(StretchProperty); }
        set { SetValue(StretchProperty, value); }
    }
    
    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(ImageButton), new PropertyMetadata(default));

    public double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty); }
        set { SetValue(StrokeThicknessProperty, value); }
    }
}
