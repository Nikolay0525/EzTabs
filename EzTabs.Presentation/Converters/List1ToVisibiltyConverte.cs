using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EzTabs.Presentation.Converters
{
    public class List1ToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection list)
            {
                // If the list is empty, collapse visibility; otherwise, make it visible
                return list.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
