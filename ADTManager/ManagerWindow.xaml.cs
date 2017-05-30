using FirstFloor.ModernUI.Windows.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ADTManager {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class ManagerWindow : ModernWindow {
		public ManagerViewModel mvm;
		public ManagerWindow() {
			InitializeComponent();
		}

		~ManagerWindow() {
			mvm.Dispose();
		}

		private void OverlayedButton2_Click(object sender, RoutedEventArgs e) {
			var a = 0;
		}

		protected override void OnClosing(CancelEventArgs e) {
			mvm.Dispose();
			base.OnClosing(e);
		}
	}
}
