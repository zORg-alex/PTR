using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ADTManager {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		ManagerWindow Window;
		ManagerViewModel Mvm;
		public App() {

			//ConsoleManager.Show();

			Window = new ManagerWindow();
			Mvm = new ManagerViewModel(Window);
			Window.mvm = Mvm;
			Window.DataContext = Mvm;
			Window.Show();
		}

		protected override void OnExit(ExitEventArgs e) {
			Mvm.Dispose();
			base.OnExit(e);
		}
	}
}
