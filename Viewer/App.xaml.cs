using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PTR.Viewer {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		MainViewModel mvm;

		private static App _instance;
		public static App Instance {
			get { return _instance; }
			set { _instance = value; }
		}

		protected override void OnStartup(StartupEventArgs e) {
			//Set Localization
			System.Threading.Thread.CurrentThread.CurrentUICulture =
			new System.Globalization.CultureInfo("lv");

			ConsoleManager.Show();


            var w = new MainWindow();
            //mvm = new MainViewModel();
            mvm = new MainViewModel(true);
            w.DataContext = mvm;
			w.Show();
		}

        protected override void OnExit(ExitEventArgs e) {
            mvm.SaveToDB();
            base.OnExit(e);
        }
    }
}
