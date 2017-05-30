using ADTManager.WcfCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ADTManager.WcfCommunication {
	public class WcfServiceCallback : ADTManager.WcfCommunication.IWcfServiceCallback, IDisposable {
		WcfServiceClient proxy;
		Action<string, string> ConsoleWrite;

		public WcfServiceCallback(Action<string, string> ConsoleWriteAction) {
            ConsoleWrite = ConsoleWriteAction;
		}

		public void SetClient(WcfServiceClient Client) {
			proxy = Client;
		}

		public void WriteToConsole(string ConsoleName, string Text) {
			ConsoleWrite(ConsoleName, Text);
		}

		public void Dispose() {
			try {
				proxy.Close();
			} catch (Exception) {
				proxy.Abort();
			}
		}

		public void SettingsChanged(bool IsChangePending) {
			//??ServerViewModel.SettingsChanged = IsChangePending;
		}
	}
}