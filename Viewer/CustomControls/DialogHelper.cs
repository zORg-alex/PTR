using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace PTR.Viewer.CustomControls {
	public class DialogHelper : DependencyObject {
		public static string OpenDialog(string Type, string Filter, string Path) {
			FileDialog dlg = new OpenFileDialog();
            var fdlg = new System.Windows.Forms.FolderBrowserDialog();
            if (Type.ToLower() == "save") {
                dlg = new SaveFileDialog();
            }
            if (Type.ToLower() == "selectfolder") {
                fdlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
                fdlg.SelectedPath = Path;
                fdlg.Description = Filter;

                var fdres = fdlg.ShowDialog();

                if (fdres == System.Windows.Forms.DialogResult.OK || fdres == System.Windows.Forms.DialogResult.Yes) {
                    return fdlg.SelectedPath;
                }
                return "";
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
			return "";
		}
	}
}
