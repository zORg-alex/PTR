﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PTR.PTRLib {
	public class UVMCommandHost {// : ICommand {
		List<UVMCommand> list = new List<UVMCommand>();

		public void Add(UVMCommand Command) {
			list.Add(Command);
		}

		public UVMCommand Get(string Name) {
			var c = list.Find(m => m.Name == Name);
			return c;
		}
	}

	public class UVMCommandHostToUVMCommandConverter : IValueConverter {
		public object Convert(object value,Type targetType, object parameter, CultureInfo culture) {
			if (value as UVMCommandHost == null) return null;
			return (value as UVMCommandHost).Get(parameter as string);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
