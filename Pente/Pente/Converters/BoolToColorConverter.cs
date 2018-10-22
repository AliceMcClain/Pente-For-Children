using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Pente.Converters
{
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(bool) && targetType == typeof(Brush) && parameter.GetType() == typeof(bool))
            {
                bool boolValue = (bool)value;
                bool boolParameter = (bool)parameter;
                // if both values are true, it is that players turn and therefore the box should be the highlight color
                if (boolValue == boolParameter)
                {
                    return new SolidColorBrush(Color.FromArgb(200, 255, 255, 70));
                }
                else
                {
                    return Brushes.Transparent;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
