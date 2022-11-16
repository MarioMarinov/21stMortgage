using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;

namespace _21stMortgageInterviewApplication
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = Brushes.Black;
            if (value != null && int.TryParse(value.ToString(), out int number))
            {
                res = (number >= 0) ? Brushes.Green : Brushes.Red;
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
