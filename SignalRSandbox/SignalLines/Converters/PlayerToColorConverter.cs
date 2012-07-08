using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SignalLines.Converters
{
    public class PlayerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;

            if (id > 0) return Brushes.Pink;

            return Brushes.MediumPurple;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}