using System;
using System.Globalization;
using System.Windows.Data;

namespace VolumeScroller
{
    /// <summary>
    /// Converts between an Enum value and a boolean value for use with RadioButtons
    /// </summary>
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
                return false;

            string parameterString = parameter.ToString();
            string valueString = value.ToString();

            return parameterString.Equals(valueString, StringComparison.OrdinalIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !(bool)value)
                return Binding.DoNothing;

            return Enum.Parse(targetType, parameter.ToString());
        }
    }
}