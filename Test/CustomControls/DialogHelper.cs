using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PTR.Test.CustomControls {
	public class DialogHelper : DependencyObject {
		public static string OpenDialog(string Type, string Filter, string Path) {
			FileDialog dlg;
			if (Type.ToLower() == "save") {
				dlg = new SaveFileDialog();
			}
			else {
				dlg = new OpenFileDialog();
			}
			dlg.InitialDirectory = Path;
			//Filter string should contain a description of the filter, 
			//followed by a vertical bar and the filter pattern. 
			//Must also separate multiple filter description and 
			//pattern pairs by a vertical bar. Must separate multiple 
			//extensions in a filter pattern with a semicolon. Example: 
			// "Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*"
			dlg.Filter = Filter;
			if (dlg.ShowDialog() == true) {
				return dlg.FileName;
			}
			else {
				return "";
			}
			return "";
		}
	}
}
