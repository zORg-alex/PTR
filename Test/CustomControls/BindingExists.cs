using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PTR.Test.CustomControls {
	public class BindingExists : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value == DependencyProperty.UnsetValue) {
				// perhaps do something
				return false;
			}
			else if (value == null) {
				// perhaps do something else
				return false;
			}

			return true;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
