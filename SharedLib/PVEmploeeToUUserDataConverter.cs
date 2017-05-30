using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PTR.PTRLib {
	public class PVEmploeeToUUserDataConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if ((PVEmploee)value == null) return null;
			return ((PVEmploee)value).UUser;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if ((UUser)value == null) return null;
			return ((UUser)value).PVEmploees.FirstOrDefault();
		}
	}
}
