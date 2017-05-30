using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ADTService {
	public partial class ADTWindowsService : ServiceBase {

		public static ServiceHost ServiceWCFHost;
		private System.Timers.Timer timer;
		private Thread ADTThread = new Thread((p) => ADT.Main(null)) { Name = "ADT Service Thread" };

		public ADTWindowsService() {
			InitializeComponent();
			// Name the Windows Service
			ServiceName = "ADTService";
		}
		
		public static void Main() {
			bool Debug = false;
            if (Debug) {
                bool exit = false;
                var adt = new ADTWindowsService();
                adt.OnStart(null);
                while (!exit) {
                    //exit = true;
                    Thread.Sleep(1000);
                    //if (ServiceWCFHost.State == CommunicationState.Faulted || ServiceWCFHost.State == CommunicationState.Closed) exit = true;
                }
                adt.OnStop();
            } else {
                ServiceBase.Run(new ADTWindowsService());
            }
        }

		protected override void OnStart(string[] args) {
			#region Set ServiceState to StartPending
			// Update the service state to Start Pending.
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
			#endregion

			if (ServiceWCFHost != null) {
				ServiceWCFHost.Close();
			}
			// Create a WCF service host to receive a connection to a WindowsService client
			//Uri baseAddress = new Uri("http://localhost/PTR/WcfService");
			ServiceWCFHost = new ServiceHost(typeof(ADTService.WcfCommunication.WcfService));
			try {
				ServiceWCFHost.Open();
			} catch (Exception e) {
				ServiceWCFHost.Abort();
			}
			//Service code Here
			ADTThread.Start();

			#region Update the ServiceState to Running
			serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
			#endregion
		}

		private void OnTimer(object sender, ElapsedEventArgs e) {
		}

		protected override void OnStop() {
			ADT.exit = true;
			////Send Manager a singnal that service is stopping
			//if (ADTServiceWCF.instance != null) ADTServiceWCF.instance.Disconnect();
		}

		protected override void OnPause() {
			base.OnPause();
		}

		protected override void OnContinue() {
			base.OnContinue();
		}

		#region ServiceStatus
		public enum ServiceState {
			SERVICE_STOPPED = 0x00000001,
			SERVICE_START_PENDING = 0x00000002,
			SERVICE_STOP_PENDING = 0x00000003,
			SERVICE_RUNNING = 0x00000004,
			SERVICE_CONTINUE_PENDING = 0x00000005,
			SERVICE_PAUSE_PENDING = 0x00000006,
			SERVICE_PAUSED = 0x00000007,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ServiceStatus {
			public long dwServiceType;
			public ServiceState dwCurrentState;
			public long dwControlsAccepted;
			public long dwWin32ExitCode;
			public long dwServiceSpecificExitCode;
			public long dwCheckPoint;
			public long dwWaitHint;
		};

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
		#endregion
	}
}
