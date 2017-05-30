namespace ADTService {
	partial class ProjectInstaller {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.ADTServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.ADTService = new System.ServiceProcess.ServiceInstaller();
			// 
			// ADTServiceProcessInstaller
			// 
			this.ADTServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.ADTServiceProcessInstaller.Password = null;
			this.ADTServiceProcessInstaller.Username = null;
			// 
			// ADTService
			// 
			this.ADTService.DelayedAutoStart = true;
			this.ADTService.Description = "Active Directory Tracking Service";
			this.ADTService.DisplayName = "ADT";
			this.ADTService.ServiceName = "ADT Service";
			this.ADTService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ADTServiceProcessInstaller,
            this.ADTService});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller ADTServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller ADTService;
	}
}