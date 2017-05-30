using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADTService {
	class ADTSyncTask {
		static internal int Delay = 30000;
		static internal bool exit = false;
		public ADTSyncTask() {

		}

		public void Main() {
			while (!exit) {
				//Here go all things that should happen once in a while
				Thread.Sleep(Delay);
				ADTConsole.Cleanup();
				//DB does not need to be saved here since it gets saved every time by UserScanner and ADScanners
			}
		}
	}
}
