using ADTManager.WcfCommunication;
using PTR.PTRLib;
using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading;
using static ADTManager.ManagerViewModel;

namespace ADTManager {
	public class ServerViewModel : Notifiable, IDisposable {
		ManagerViewModel parent;
		WcfServiceClient WcfService;
		private string name;
		public string Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }

		private string address;
		public string Address {
			get { return address; }
			set {
				if (address != value) {
					address = value;
					if (ConnectWorker != null) ConnectWorker.Abort();
					if (WcfService != null) WcfService.Abort();
					ConsoleLogs = new List<ConsoleLog>();
					ConsoleLogs.Add(new ConsoleLog() { Name = "ADTUserScanner" });
					//if (ConnectWorker != null) {
						//if (ConnectWorker.ThreadState != ThreadState.Aborted) ConnectWorker.Start();
						//else
							StartConnectWorker();
					//}
					RaisePropertyChanged("Address");
				}
			}
		}

		private bool selected;
		public bool Selected { get { return selected; } set { selected = value; RaisePropertyChanged("Selected"); } }

		private bool remove;
		public bool Remove { get { return remove; } set { remove = value; RaisePropertyChanged("Remove"); } }

		public ServerViewModel() {
			//TestData
			Name = "testName";
			Address = "TestAddress";
		}

		public void AddADScannerLog(string Name) {
			AddADScannerLog(new ConsoleLog() { Name = Name });
		}

		public void AddADScannerLog(ConsoleLog log) {
			var list = new List<ConsoleLog>();
			list.AddRange(ConsoleLogs);
			list.Add(log);
			ConsoleLogs = list;
		}

		public ServerViewModel(ManagerViewModel Parent, string Name, string Address) {
			parent = Parent;
			this.Name = Name;
			this.Address = Address;
			StartConnectWorker();

			comOpenSettingsWindow = new UVMCommand("OpenSettingsWindow", (p) => {
				SettingsViewModel svm = new SettingsViewModel(this.Name, this.Address, (_name, _address, _remove) => {
					this.Remove = _remove;
					this.Name = _name;
					this.Address = _address;
					parent.ConfigChangedInside = true;
				});
				SettingsWindow sw = new SettingsWindow(svm);
				sw.Owner = parent.window;
				sw.Show();
			});
		}

		private void StartConnectWorker() {
			if (ConnectWorker != null) {
				ConnectWorker.Abort();
				ConnectWorker = null;
			}
			ConnectWorker = new Thread(() => {
				if (Address == "") return;
				InstanceContext c = new InstanceContext(new WcfServiceCallback(ConsoleWrite));

				WcfService = new WcfServiceClient(c, "WSDualHttpBinding_IWcfService", "http://" + Address + "/PTR/WcfService");
				//c.SetClient(WcfService);
				try {
					WcfService.Open();
					var cache = WcfService.Connect();
					if (cache.Consoles != null) {
						foreach (var cl in cache.Consoles) {
							foreach (var l in cl.Lines) {
								ConsoleWrite(cl.Name, l);
							}
						}
					} else {
						ConsoleWrite("Exceptions", "A problem occured while connecting to a service, possibly Wcf reference is configured incorectly");
					}
				} catch (Exception e) {
					WcfService.Abort();
					parent.StatusIcon = StatusIcons.Error;
					parent.StatusMessage = e.Message;
				}
				Selected = true;
			}) { Name = "ServerViewMovel ConnectWorker " + Name };
			ConnectWorker.Start();
		}

		~ServerViewModel() {
			Dispose();
		}

		public Thread ConnectWorker;

		public void Dispose() {
			if (ConnectWorker != null) ConnectWorker.Abort();
			if (WcfService != null) {
				try {
					WcfService.Disconnect();
					WcfService.Close();
				} catch (Exception e) {
					WcfService.Abort();
					parent.StatusIcon = StatusIcons.Error;
					parent.StatusMessage = e.Message;
				}
			}
			consoleLogs.Clear();
		}

		public void ConsoleWrite(string ConsoleName, string Text) {
			var l = ConsoleLogs.Find(g => g.Name == ConsoleName);
			if (l == null) {
				l = new ConsoleLog() { Name = ConsoleName, Log = Text };
				AddADScannerLog(l);
			} else l.Log += Text;
		}

		internal bool settingsChanged;
		public bool SettingsChanged { get { return settingsChanged; } set {
				settingsChanged = value;
				if (parent != null) parent.SaveConfig();//In case of Test
				RaisePropertyChanged("SettingsChanged"); } }
		
		public UVMCommand comOpenSettingsWindow { get; set; }
		
		internal List<ConsoleLog> consoleLogs= new List<ConsoleLog>();
		public List<ConsoleLog> ConsoleLogs { get { return consoleLogs; } set { consoleLogs = value; RaisePropertyChanged("ConsoleLogs"); } }

	}

	public class ConsoleLog : Notifiable {
		private string name = "";
		public string Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }

		private string log = "";
		public string Log { get { return log; } set { log = value; RaisePropertyChanged("Log"); } }
	}
}