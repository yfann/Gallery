using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Gallery.Converter
{
    internal class TagListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<string> list = value as IEnumerable<string>;
            if (list != null)
            {
                return string.Join(" ", list.ToArray());
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string temp = value as string;
            if (!string.IsNullOrEmpty(temp))
            {
                Regex re = new Regex(@"\s+");
                temp = re.Replace(temp, " ");
                string[] list = temp.Split(' ');

                return new List<string>(list);
            }
            else
            {
                return null;
            }
        }
    }
}