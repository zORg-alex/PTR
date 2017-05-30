using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADTService {
	class ADT {
		static internal PTRContext db = new PTRContext();
		static internal bool UsersReady = false;
		static internal int Delay = 1000;
        static internal bool skipADUserScanning = false;
        static internal bool skipFolderParsing = false;
        static internal bool ex = false;
        static internal bool exit { get { return ex; } set { ex = value; } }
        static internal List<ADTScanner> ScannerThreads = new List<ADTScanner>();
		static internal List<ADTScanner> ScannerThreadsToStart = new List<ADTScanner>();
		static internal List<ADTScanner> ScannerThreadsToStop = new List<ADTScanner>();
		static internal Thread UserScannerThread;
		static internal Thread SyncTaskThread;
		static internal SettingsReader SettingsReader = new SettingsReader();
		static internal bool SettingsChanged = false;

		static public void Main(string[] args) {
            ////For debug time only
            //bool exit = false;
            //while (!exit) {
            //	//exit = true;
            //	Thread.Sleep(5000);
            //}

            if (!skipADUserScanning) {
                ADTUserScanner UserScanner = new ADTUserScanner();
                UserScannerThread = new Thread(() => { UserScanner.Main(); });
                UserScannerThread.Name = "ADUserScannerThread";
                UserScannerThread.Start();
                //UsersReady = true; //For Debug only
            }

            ADTSyncTask SyncTask = new ADTSyncTask();
			SyncTaskThread = new Thread(() => { SyncTask.Main(); });
			SyncTaskThread.Name = "SyncTaskThread";
			SyncTaskThread.Start();

			while (!exit) {
				if (SettingsChanged) {
					SettingsReader.Read();
					SettingsChanged = false;
				}
				Thread.Sleep(Delay);
				if (ScannerThreadsToStart.Count > 0) {
					foreach (var st in ScannerThreadsToStart) StartScanner(st);
					ScannerThreadsToStart.Clear();
				}
				if (ScannerThreadsToStop.Count > 0) {
					foreach (var s in ScannerThreadsToStop) {
						var st = ScannerThreads.Find(t => t.rootPath == s.rootPath && t.rootPathDB == s.rootPathDB);
						StopScanner(st);
					}
					ScannerThreadsToStop.Clear();
				}
			}
			ADTSyncTask.exit = true;
			ADTSyncTask.Delay = 1;
            ADTUserScanner.exit = true;
			foreach (var st in ScannerThreads) {
				st.exit = true;
			}
			db.SaveChanges();
		}

		static internal void StartScanner(ADTScanner ADTScanner) {
			//TODO Check if such exist

			Thread T = new Thread(() => {
				ADTScanner.Main();
			}) { Name = "ADTScanner " + ADTScanner.rootPathDB };
			ADTScanner.Thread = T;
			ScannerThreads.Add(ADTScanner);
			T.Start();
			return;
		}

		static internal void StopScanner(ADTScanner ADTScanner) {
			//Finish thread if it is in a watching state, or abort if still scanning
			//Any mess will be cleaned with the next scan
			if (ADTScanner.FinishedScanning)
				ADTScanner.exit = true;
			else
				ADTScanner.Thread.Abort();
			ScannerThreads.Remove(ADTScanner);
		}
	}
}
