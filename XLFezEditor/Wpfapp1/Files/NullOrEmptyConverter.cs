using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace XLFezEditor.Files
{
    public class NullOrEmptyConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }
            if(value.Length > 0)
            {
            TransUnit obj = (TransUnit)value[1];

            if (obj.Meaning == null || String.IsNullOrWhiteSpace(obj.Meaning))
            {
                return obj.Description;
            }

            return obj.Meaning;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
