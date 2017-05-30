using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ADTService.WcfCommunication {
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
	public interface IWcfService {
		[OperationContract]
		ADTServiceOnConnectData Connect();
		[OperationContract]
		void Disconnect();
	}
	
	public interface IServiceCallback {
		[OperationContract]
		void WriteToConsole(string ConsoleName, string Text);
		
		[OperationContract]
		void SettingsChanged(bool IsChangePending);
	}

	[DataContract]
	public class ADTServiceOnConnectData {
		private List<ConsoleLog> consoles = new List<ConsoleLog>();

		[DataMember]
		public List<ConsoleLog> Consoles {
			get { return consoles; }
			set { consoles = value; }
		}
		
		public class ConsoleLog {
			public List<string> Lines = new List<string>();
			public string Name;
		}
	}
}
