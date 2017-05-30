using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ADTManager {
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window {
		private SettingsViewModel svm;
		public SettingsViewModel Svm {
			get { return svm; }
			set { svm = value; }
		}

		bool MouseIn = false;

		public SettingsWindow() {

		}

		public SettingsWindow(SettingsViewModel ViewModel) {
			DataContext = Svm = ViewModel;
			Svm.w = this;
			InitializeComponent();
		}

		private void ExportMenuWindow_MouseEnter(object sender, MouseEventArgs e) {
			MouseIn = true;
		}

		private void ExportMenuWindow_MouseLeave(object sender, MouseEventArgs e) {
			MouseIn = false;
		}

		private void ExportMenuWindow_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
			try {
				if (!MouseIn) Close();
			} catch { }
		}

		private void ExportMenuWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Escape) Close();
		}

		protected override void OnClosing(CancelEventArgs e) {
			base.OnClosing(e);
		}

		private void AddressTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
			try { System.Diagnostics.Process.Start("http://" + Svm.HostAddr + "/PTR/WcfService"); }
			catch { }
		}
		
		private void Cancel_Click(object sender, RoutedEventArgs e) {
			Close();
		}
	}
}
