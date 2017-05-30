using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PTR.PTRLib {

    partial class ImportReport {
		private static UVMCommandHost _commands;
		public static UVMCommandHost Commands {
			get {
				if (_commands == null) _commands = new UVMCommandHost();
				return _commands;
			}
			set { _commands = value; }
		}
	}
}
