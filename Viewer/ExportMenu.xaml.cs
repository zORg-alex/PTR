using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PTR.Viewer {
	/// <summary>
	/// Interaction logic for ExportMenu.xaml
	/// </summary>
	public partial class ExportMenu : Window {

		private ExportViewModel xvm;
		public ExportViewModel Xvm {
			get { return xvm; }
			set { xvm = value; }
		}

		public ExportMenu() {

		}

		public ExportMenu( ExportViewModel ViewModel) {
			DataContext = Xvm = ViewModel;
			InitializeComponent();
		}
		
		private void ExportMenuWindow_MouseEnter(object sender, MouseEventArgs e) {
			Xvm.MouseIn = true;
		}

		private void ExportMenuWindow_MouseLeave(object sender, MouseEventArgs e) {
			Xvm.MouseIn = false;
		}

		private void ExportMenuWindow_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
			try {
				//if (!Xvm.MouseIn) Close();
			}
			catch { }
		}

		private void BrowseButton_Click(object sender, RoutedEventArgs e) {
			var dialog = new System.Windows.Forms.FolderBrowserDialog();
			System.Windows.Forms.DialogResult result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK) Xvm.ExportPath = dialog.SelectedPath + "\\";
		}

		private void ExportMenuWindow_KeyDown(object sender, KeyEventArgs e) {
			//if (e.Key == Key.Escape) Close();
		}
	}
}
