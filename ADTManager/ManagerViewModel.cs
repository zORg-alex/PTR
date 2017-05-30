using PTR.PTRLib;
using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;

namespace ADTManager {
	public class ManagerViewModel : Notifiable, IDisposable {

		public ManagerWindow window;
		private FileSystemWatcher configWatcher;

		private List<ServerViewModel> serverViewModels = new List<ServerViewModel>();
		public List<ServerViewModel> ServerViewModels { get { return serverViewModels; } set { serverViewModels = value; RaisePropertyChanged("ServerViewModels"); } }

		private ServerViewModel selectedServerViewModel;
		public ServerViewModel SelectedServerViewModel { get { return selectedServerViewModel; } set { selectedServerViewModel = value; RaisePropertyChanged("SelectedServerViewModel"); } }

		private string statusMessage;
		public string StatusMessage { get { return statusMessage; } set { statusMessage = value; RaisePropertyChanged("StatusMessage"); } }

		private StatusIcons statusIcon;
		public StatusIcons StatusIcon { get { return statusIcon; } set { statusIcon = value; RaisePropertyChanged("StatusIcon"); } }
		public enum StatusIcons { None, Warning, Error }

		public ManagerViewModel() {
			var s = new ServerViewModel() { Name = "TestServer" };
			ServerViewModels.Add(s);
			SelectedServerViewModel = s;
		}

		public ManagerViewModel(ManagerWindow Window) {
			window = Window;
			//SettingsReader = new SettingsReader(this);
			if (serverViewModels.Count > 0) SelectedServerViewModel = ServerViewModels[0];
			comAddServer = new UVMCommand("AddServer", p => {
				ServerViewModel svm = new ServerViewModel(this, "Server Name", "Address");
				List<ServerViewModel> list = new List<ServerViewModel>();
				list.AddRange(serverViewModels);
				list.Add(svm);
				ServerViewModels = list;
			});

			ReadConfig();
			//Setup Settings file watcher
			configWatcher = new FileSystemWatcher();
			configWatcher.Path = System.AppDomain.CurrentDomain.BaseDirectory;
			configWatcher.NotifyFilter = NotifyFilters.LastWrite;
			configWatcher.Filter = "*.xml";
			configWatcher.Changed += new FileSystemEventHandler((o, args) => { ConfigChangedOutside = true; });
			configWatcher.EnableRaisingEvents = true;
		}

		~ManagerViewModel() {
			Dispose();
		}

		public void Dispose() {
			foreach (var s in ServerViewModels) {
				s.Dispose();
			}
		}

		private bool configChangedOutside;
		public bool ConfigChangedOutside {
			get { return configChangedOutside; }
			set {
				configChangedOutside = value;
				RaisePropertyChanged("ConfigChangedOutside");
				if (value == true) ReadConfig();
			}
		}

		private bool configChangedInside;
		public bool ConfigChangedInside {
			get { return configChangedInside; }
			set {
				configChangedInside = value;
				RaisePropertyChanged("ConfigChangedInside");
				if (value == true) SaveConfig();
			}
		}

		internal void SaveConfig() {
			var list = new List<ServerViewModel>();
			list.AddRange(ServerViewModels.FindAll(s=>!s.Remove));
			ServerViewModels = list;
			foreach (var s in list) {
				s.Dispose();
			}
			//TODO Save Server configs to xml
			XmlDocument doc = new XmlDocument();
			var config = doc.CreateElement("config");
			doc.AppendChild(config);
			foreach (var s in serverViewModels) {
				var e = doc.CreateElement("service");
				var name = doc.CreateAttribute("name");
				name.Value = s.Name;
				var addr = doc.CreateAttribute("address");
				addr.Value = s.Address;
				e.Attributes.Append(name);
				e.Attributes.Append(addr);
				config.AppendChild(e);
			}
			configWatcher.EnableRaisingEvents = false;
			doc.Save(System.AppDomain.CurrentDomain.BaseDirectory + "ADTManagerConfig.xml");
			configWatcher.EnableRaisingEvents = true;

			ConfigChangedInside = false;
		}

		internal void ReadConfig() {
			//while (ConfigChangedInside) {
			//	Thread.Sleep(10);//Anyway it's a Worker Thread
			//}
			List<Server> foundServers = new List<Server>();

			XmlDocument doc = new XmlDocument();
			XmlReader reader = XmlReader.Create(System.AppDomain.CurrentDomain.BaseDirectory + "ADTManagerConfig.xml");
			doc.Load(reader);
			reader.Dispose();

			XmlNode services = doc.FirstChild;
			foreach (XmlNode xs in services.ChildNodes) {
				var s = new Server();
				foreach (XmlAttribute a in xs.Attributes) {
					if (a.Name.ToLower() == "name") s.Name = a.Value;
					if (a.Name.ToLower() == "address") s.Addr = a.Value;
				}
				foundServers.Add(s);
			}

			//using (XmlReader reader = XmlReader.Create(System.AppDomain.CurrentDomain.BaseDirectory + "ADTManagerConfig.xml")) {
			//	XmlWriterSettings ws = new XmlWriterSettings();
			//	ws.Indent = true;
			//	while (reader.Read()) {
			//		if (reader.NodeType == XmlNodeType.Element) {
			//			if (reader.Name.ToLower() == "service") {
			//				var s = new Server();
			//				for (int i = 0; i < reader.AttributeCount; i++) {
			//					reader.MoveToNextAttribute();
			//					if (reader.Name.ToLower() == "name") s.Name = reader.Value;
			//					if (reader.Name.ToLower() == "address") s.Addr = reader.Value;
			//				}
			//				foundServers.Add(s);
			//			}
			//		}
			//	}
			//}

			//Compare and Create&Add|Remove
			foreach (var s in foundServers) {
				if (ServerViewModels.Find(t => (t.Name == s.Name && t.Address == s.Addr)) == null) {
					ServerViewModel svm = new ServerViewModel(this, s.Name, s.Addr);
					var list = new List<ServerViewModel>();
					list.AddRange(ServerViewModels);
					list.Add(svm);
					ServerViewModels = list;
				}
			}
			List<ServerViewModel> toRemove = new List<ServerViewModel>();
			foreach (var s in ServerViewModels) {
				if (foundServers.Find(t => (t.Name == s.Name && t.Addr == s.Address)) == null) {
					toRemove.Add(s);
				}
			}
			foreach (var r in toRemove) {
				var list = new List<ServerViewModel>();
				list.AddRange(ServerViewModels);
				list.Remove(r);
				ServerViewModels = list;
				r.Dispose();
			}
			configChangedOutside = false;
		}

		class Server {
			public string Name = "";
			public string Addr = "";
		}

		public UVMCommand comAddServer { get; set; }
	}
}
