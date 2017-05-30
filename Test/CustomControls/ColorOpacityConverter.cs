using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PTR.Test.CustomControls {
	public class ColorOpacityConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (null == value) {
				return value;
			}
			double a;
			if (!double.TryParse((string)parameter, out a)) return value;
			//double a = Math.Min(Math.Max((double)parameter, 1), 0);
			// For a more sophisticated converter, check also the targetType and react accordingly..
			if (value is Color) {
				var color = (Color)value;
				return Color.FromArgb((byte)Math.Round((double)parameter * 255), color.R, color.G, color.B);
			}
			if (value is SolidColorBrush) {
				var color = ((SolidColorBrush)value).Color;
				return new SolidColorBrush(Color.FromArgb((byte)Math.Round(a * 255), color.R, color.G, color.B));
			}
			// You can support here more source types if you wish
			// For the example I throw an exception

			Type type = value.GetType();
			throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
