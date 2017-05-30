using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ADTService.WcfCommunication {
	// Could be better to move it to ADTService.WcfCommunication
	public class WcfService : IWcfService {

		public ADTServiceOnConnectData Connect() {
			ADTConsole.SetCallback();
			var r = new ADTServiceOnConnectData();
			foreach (var cl in ADTConsole.GetCache()) {
				r.Consoles.Add(new ADTServiceOnConnectData.ConsoleLog() { Name = cl.Name, Lines = cl.Lines });
			}
			return r;
		}

		public void Disconnect() {
            ADTConsole.RemoveCallback();
		}
	}
}
