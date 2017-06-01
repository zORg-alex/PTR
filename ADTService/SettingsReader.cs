using PTR.PTRLib;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ADTService {
	class SettingsReader {
		static protected PTRContext db = new PTRContext();
        //static protected bool dbLocked { get { return Program.ContextLock; } set { Program.ContextLock = value; } }
        static protected int Delay { get { return ADT.Delay; } set { ADT.Delay = value; } }
        static protected bool skipADUserScanning { get { return ADT.skipADUserScanning; } set { ADT.skipADUserScanning = value; } }
        static protected bool skipFolderParsing { get { return ADT.skipFolderParsing; } set { ADT.skipFolderParsing = value; } }
        static protected string LogPath { get { return ADT.LogPath; } set { ADT.LogPath = value; } }
        static protected FileSystemWatcher watcher;

		public SettingsReader() {
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
			ADT.SettingsChanged = true;
			//ADTManagerWCFClient.SetSettingsChanged(true);
		}

		public void Read() {
			using (XmlReader reader = XmlReader.Create(System.AppDomain.CurrentDomain.BaseDirectory + "ADTrackerConfig.xml")) {
				XmlWriterSettings ws = new XmlWriterSettings();
				ws.Indent = true;
				List<ADTScanner> currentScanners = new List<ADTScanner>();
				while (reader.Read()) {
					if (reader.NodeType == XmlNodeType.Element) {
						if (reader.Name.ToLower() == "dir") {
							//New Tracker Thread
							string path = "";
							string pathDB = "";
							for (int i = 0; i<reader.AttributeCount; i++) {
								reader.MoveToNextAttribute();
								if (reader.Name.ToLower() == "path") path = reader.Value;
								if (reader.Name.ToLower() == "pathdb") pathDB = reader.Value;
							}
							currentScanners.Add(new ADTScanner(path, pathDB));
						}
						if (reader.Name.ToLower() == "config") {
							for (int i = 0; i < reader.AttributeCount; i++) {
								reader.MoveToNextAttribute();
                                if (reader.Name.ToLower() == "delay") Delay = int.Parse(reader.Value);
                                if (reader.Name.ToLower() == "skipaduserscanning") skipADUserScanning = bool.Parse(reader.Value);
                                if (reader.Name.ToLower() == "skipfolderparsing") skipFolderParsing = bool.Parse(reader.Value);
                                if (reader.Name.ToLower() == "logpath") LogPath = reader.Value;
                                //if (Delay < 50) Delay = 50;
                            }
						}
					}
				}
				//Compare running, run and stop
				foreach (var s in currentScanners) {
					if (ADT.ScannerThreads.Find(t => (t.rootPath == s.rootPath && t.rootPathDB == s.rootPathDB)) == null)
						ADT.ScannerThreadsToStart.Add(s);
				}
				foreach (var s in ADT.ScannerThreads) {
					if (currentScanners.Find(t => (t.rootPath == s.rootPath && t.rootPathDB == s.rootPathDB)) == null)
						ADT.ScannerThreadsToStop.Add(s);
				}
			}
		}
	}
}