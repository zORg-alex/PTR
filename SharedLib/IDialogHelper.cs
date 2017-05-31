using PTR.PTRLib.Common;
using System;

namespace PTR.PTRLib {
	public interface IDialogHelper {
		Func<string, string, string, string> OpenDialog { get; set; }
	}
}