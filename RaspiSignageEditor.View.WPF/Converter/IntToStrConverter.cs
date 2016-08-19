using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RaspiSignageEditor.View.WPF.Converter
{
    [ValueConversion(typeof(int), typeof(String))]
    class IntToStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ivalue = value as int?;
            if (ivalue != null)
            {
                return ivalue.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            int result;

            if (str != null && int.TryParse(str, out result))
            {
                return result;
            }
            return -1;
        }
    }
}
