using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PTR.Viewer.CustomControls {
    public class RemainderConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return value;
            }
            int p = (int)value;
            string par = (string)parameter;
            int d = int.Parse(par);
            int r = p % d;
            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}