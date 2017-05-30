using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PTR.Viewer.CustomControls {
    public class AnyToBoolConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return false;
            }
            if (typeof(int) == value.GetType()) {
                int p = 0;
                int.TryParse(parameter as string, out p);
                return (int)value == p ? false : true;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
