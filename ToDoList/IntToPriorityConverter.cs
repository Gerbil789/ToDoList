using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PresentationLayer.Converters
{
    public class IntToPriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                switch (intValue)
                {
                    case 0:
                        return "Urgent";
                    case 1:
                        return "Important";
                    case 2:
                        return "Nomral";
                    case 3:
                        return "Minor";
                    default:
                        return "Normal";
                }
            }
            return "Nomral";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
