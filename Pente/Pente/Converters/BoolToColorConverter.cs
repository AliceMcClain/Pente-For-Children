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
            if(value.GetType() == typeof(int) && targetType == typeof(Brush) && parameter.GetType() == typeof(bool))
            {
                int intValue = (int)value;
                bool boolParameter = (bool)parameter;
                // if intValue % 2 is edd it is the first player's turn so parameter need to true to have color
                if ((intValue % 2 == 1) == boolParameter)
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
