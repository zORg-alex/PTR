using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ADTManager {
	class SettingsReader {
		static protected PTRContext db;
		//static protected bool dbLocked { get { return Program.ContextLock; } set { Program.ContextLock = value; } }
		protected int Delay = 1000;
		protected FileSystemWatcher watcher;
		public ManagerViewModel Mvm;

		public SettingsReader(ManagerViewModel ManagerViewModel) {
			db = new PTRContext();
			Mvm = ManagerViewModel;
			Read();
			//Setup Settings file watcher
			watcher = new FileSystemWatcher();
			watcher.Path = System.AppDomain.CurrentDomain.BaseDirectory;
			watcher.NotifyFilter = NotifyFilters.LastWrite;
			watcher.Filter = "*.xml";
			watcher.Changed += new FileSystemEventHandler(SettingsChanged);
			watcher.EnableRaisingEvents = true;
		}

		private void SettingsChanged(object sender, FileSystemEventArgs e) {
			Read();
			//TODO Make main Thread read it
		}

		public void Read() {
			using (XmlReader reader = XmlReader.Create(System.AppDomain.CurrentDomain.BaseDirectory + "ADTManagerConfig.xml")) {
				XmlWriterSettings ws = new XmlWriterSettings();
				ws.Indent = true;
				List<Server> foundServers = new List<Server>();
				while (reader.Read()) {
					if (reader.NodeType == XmlNodeType.Element) {
						if (reader.Name.ToLower() == "service") {
							var s = new Server();
							for (int i = 0; i < reader.AttributeCount; i++) {
								reader.MoveToNextAttribute();
								if (reader.Name.ToLower() == "name") s.Name = reader.Value;
								if (reader.Name.ToLower() == "address") s.Addr = reader.Value;
							}
							foundServers.Add(s);
						}
					}
				}
				//Compare and Create&Add|Remove
				foreach (var s in foundServers) {
					if (Mvm.ServerViewModels.Find(t => (t.Name == s.Name && t.Address == s.Addr)) == null) {
						ServerViewModel svm = new ServerViewModel(Mvm, s.Name, s.Addr);
						var list = new List<ServerViewModel>();
						list.AddRange(Mvm.ServerViewModels);
						list.Add(svm);
						Mvm.ServerViewModels = list;
					}
				}
				List<ServerViewModel> toRemove = new List<ServerViewModel>();
				foreach (var s in Mvm.ServerViewModels) {
					if (foundServers.Find(t => (t.Name == s.Name && t.Addr == s.Address)) == null) {
						toRemove.Add(s);
					}
				}
				foreach (var r in toRemove) {
					var list = new List<ServerViewModel>();
					list.AddRange(Mvm.ServerViewModels);
					list.Remove(r);
					Mvm.ServerViewModels = list;
					r.Dispose();
				}
			}
		}
		class Server {
			public string Name = "";
			public string Addr = "";
		}
	}
}