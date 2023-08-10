using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PresentationLayer.Converters
{
    public class IntToPriorityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                switch (intValue)
                {
                    case 0:
                        return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    case 1:
                        return new SolidColorBrush(Color.FromRgb(255, 191, 41));
                    case 2:
                        return new SolidColorBrush(Color.FromRgb(109, 201, 99));
                    case 3:
                        return new SolidColorBrush(Color.FromRgb(218, 222, 217));
                    default:
                        return new SolidColorBrush(Color.FromRgb(109, 201, 99));
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
