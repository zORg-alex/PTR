using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PTR.Viewer.CustomControls {
	public class InvertBoolConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (null == value) {
				return true;
			}
            if (value.GetType() == typeof(bool)) {
                return !(bool)value;
            }
            return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return true;
            }
            if (value.GetType() == typeof(bool)) {
                return !(bool)value;
            }
            return null;
        }
	}
}
