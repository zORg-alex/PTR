using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PTR.Viewer.CustomControls {
	public class IntToGridLengthConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			double val = System.Convert.ToInt32(value);
			if (System.Convert.ToString(parameter).ToLower() == "star") {
				return new GridLength(val,GridUnitType.Star);
			}
			if (System.Convert.ToString(parameter).ToLower() == "auto") {
				return new GridLength(1, GridUnitType.Auto);
			}
			return new GridLength(val);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			GridLength val = (GridLength)value;
			if (val.GridUnitType == GridUnitType.Auto) return "Auto";
			if (val.GridUnitType == GridUnitType.Star) return val.Value + "*";
			return Math.Round(val.Value);
		}
	}
}
