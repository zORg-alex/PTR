using System;
using PTR.PTRLib;
using PTR.PTRLib.Common;

namespace ADTManager {
	public class SettingsViewModel : Notifiable {

		public SettingsWindow w;

		public SettingsViewModel(string Name, string Addr, Action<string, string, bool> Apply) {
			HostName = Name; HostAddr = Addr; apply = Apply;
			_comSave = new UVMCommand("comSave", (p) => {
				apply(HostName, HostAddr, false);
				w.Close();
			});
			_comClose = new UVMCommand("comClose", (p) => { w.Close(); });
			_comRemove = new UVMCommand("comRemove", (p) => {
				apply(HostName, HostAddr, true);
				w.Close();
			});
		}

		~SettingsViewModel() {
			try {
				w.Close();
			}
			catch { }
		}

		private string hostName;
		public string HostName { get { return hostName; } set { hostName = value; RaisePropertyChanged("HostName"); } }

		private string hostAddr;
		public string HostAddr { get { return hostAddr; } set { hostAddr = value; RaisePropertyChanged("HostAddr"); } }
		
		private static Action<string, string, bool> apply;

		public UVMCommand _comSave;
		public UVMCommand comSave { get { return _comSave; } set { _comSave = value; RaisePropertyChanged("comSave"); } }

		public UVMCommand _comClose;
		public UVMCommand comClose { get { return _comClose; } set { _comClose = value; RaisePropertyChanged("comClose"); } }

		public UVMCommand _comRemove;
		public UVMCommand comRemove { get { return _comRemove; } set { _comRemove = value; RaisePropertyChanged("comRemove"); } }
	}
}