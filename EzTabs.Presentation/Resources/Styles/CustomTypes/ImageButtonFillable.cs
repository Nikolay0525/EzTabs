using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace EzTabs.Presentation.Resources.Styles.CustomTypes
{
    public class ImageButtonFillable : ImageButton
    {
        public static readonly DependencyProperty IsFilledProperty =
        DependencyProperty.Register(nameof(IsFilled), typeof(bool), typeof(ImageButtonFillable), new PropertyMetadata(false));

        public bool IsFilled
        {
            get { return (bool)GetValue(IsFilledProperty); }
            set { SetValue(IsFilledProperty, value); }
        }
    }
}
