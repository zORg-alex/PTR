using PTR.PTRLib.Common;
using PTR.PTRLib;
using System;
using System.IO;

namespace PTR.PTRLib {
	public class ExportViewModel : Notifiable, IDialogHelper {

		//public ExportMenu w;
		/// <summary>
		/// Can't use EPP in a ViewModel without breaking a Designer, so I extracted it to a different class
		/// </summary>
		public EPPExporter EPPExporter;
		
		public ExportViewModel() {
			ExportPath = "Z:\\UserName\\Documents\\";
        }
        public ExportViewModel(object Object, Action closeAction) {
            CloseAction = closeAction;
            EPPExporter = new EPPExporter();
			//Initializing commands by giving names
			_comExportISDAVS = new UVMCommand("ExportISDAVS", (p)=> {}, true);
			_comExportPV = new UVMCommand("ExportISDAVS", (p) => {}, true);
			_comExport = new UVMCommand("Export", p => {}, true);
			switch (Object.GetType().BaseType.Name) {
				case "PVEmploee":
					UserExportType = true;
                    ShowAllowHistoricalData = true;
					comExportISDAVS.Exec = (p) => {
						EPPExporter.ExportUUser(EPPExporter.ExportType.ISDAVS, (Object as PVEmploee).UUser, AllowHistoricalData);
						Save();
					};
					comExportAD.Exec = (p) => {
						EPPExporter.ExportUUser(EPPExporter.ExportType.AD, (Object as PVEmploee).UUser, AllowHistoricalData);
						Save();
					};
					break;
				case "UUser":
					UserExportType = true;
                    ShowAllowHistoricalData = true;
                    comExportISDAVS.Exec = (p) => {
						EPPExporter.ExportUUser(EPPExporter.ExportType.ISDAVS, (Object as UUser), AllowHistoricalData);
						Save();
					};
					comExportAD.Exec = (p) => {
						EPPExporter.ExportUUser(EPPExporter.ExportType.AD, (Object as UUser), AllowHistoricalData);
						Save();
					};
					break;
				case "PVStructural":
					UserExportType = true;
                    ShowAllowHistoricalData = true;
                    comExportISDAVS.Exec = (p) => {
						EPPExporter.ExportPVStructure(EPPExporter.ExportType.ISDAVS, Object as PVStructural, AllowHistoricalData);
						Save();
					};
					comExportAD.Exec = (p) => {
						EPPExporter.ExportPVStructure(EPPExporter.ExportType.AD, Object as PVStructural, AllowHistoricalData);
						Save();
					};
					break;
				case "IFunction":
					IFunctionExportType = true;
					comExport.Exec = p => {
						EPPExporter.ExportIFunction(Object as IFunction);
						Save();
					};
					break;
				case "ADFolder":
					ADFolderExportType = true;
                    ShowAllowHistoricalData = true;
                    ShowDataExpand = true;
                    comExport.Exec = p => {
						EPPExporter.ExportADFolder(Object as ADFolder, DataExpand, AllowHistoricalData);
						Save();
					};
					break;
				default:
					break;
			}

            comExportBrowse = new UVMCommand((p) => {
                string FilePath = OpenDialog("SelectFolder", "Select Export Folder", ExportPath);
                if (FilePath == "") return;

                ExportPath = FilePath;
            });
        }
        

        private void Save() {
			//Save to *.xlsx
			byte[] bin = EPPExporter.Ep.GetAsByteArray();
			if (!Directory.Exists(ExportPath)) ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			if (ExportPath.Substring(ExportPath.Length - 1) != "\\") ExportPath = ExportPath + "\\";
			string file = ExportPath + Guid.NewGuid().ToString() + ".xlsx";
			File.WriteAllBytes(file, bin);

			if (OpenOnFinish) System.Diagnostics.Process.Start(file);
			if (OpenFolderOnFinish) System.Diagnostics.Process.Start(ExportPath);

            CloseAction();
		}

		private UVMCommand _comExportISDAVS;
		public UVMCommand comExportISDAVS {
			get { return _comExportISDAVS; }
			set { _comExportISDAVS = value; }
		}

		private UVMCommand _comExportPV;
		public UVMCommand comExportAD {
			get { return _comExportPV; }
			set { _comExportPV = value; }
		}

		private UVMCommand _comExport;
		public UVMCommand comExport {
			get { return _comExport; }
			set { _comExport = value; RaisePropertyChanged("comExport"); }
		}

		/// <summary>
		/// Helps to decide if the window lost focus and can de closed
		/// </summary>
		private bool _mouseIn;
		public bool MouseIn { get { return _mouseIn; } set { _mouseIn = value; RaisePropertyChanged("MouseIn"); } }

		public string ExportPath {
			get { return PTRLibSettings.Default.ExportPath; }
			set {
				PTRLibSettings.Default.ExportPath = value;
				PTRLibSettings.Default.Save();
				RaisePropertyChanged("ExportPath");
			}
		}

        /// <summary>
        /// Expand data in reports. Like if exporting a folder, then include all subfolders
        /// </summary>
        public bool DataExpand {
            get { return PTRLibSettings.Default.ExportExpand; }
            set {
				PTRLibSettings.Default.ExportExpand = value;
				PTRLibSettings.Default.Save();
                RaisePropertyChanged("DataExpand");
            }
        }

        private bool _showDataExpand;
        public bool ShowDataExpand { get { return _showDataExpand; } set { _showDataExpand = value; RaisePropertyChanged("ShowDataExpand"); } }

        /// <summary>
        /// Don't strip historical data off of reports
        /// </summary>
        public bool AllowHistoricalData {
            get { return PTRLibSettings.Default.ExportAllowHistoricalData; }
            set {
				PTRLibSettings.Default.ExportAllowHistoricalData = value;
				PTRLibSettings.Default.Save();
                RaisePropertyChanged("AllowHistoricalData");
            }
        }

        private bool _showAllowHistoricalData;
        public bool ShowAllowHistoricalData { get { return _showAllowHistoricalData; } set { _showAllowHistoricalData = value; RaisePropertyChanged("ShowAllowHistoricalData"); } }

        public bool OpenOnFinish {
			get { return PTRLibSettings.Default.ExportOpenFile; }
			set {
				PTRLibSettings.Default.ExportOpenFile = value;
				PTRLibSettings.Default.Save();
				RaisePropertyChanged("OpenOnFinish");
			}
		}
		
		public bool OpenFolderOnFinish {
			get { return PTRLibSettings.Default.ExportOpenFolder; }
			set {
				PTRLibSettings.Default.ExportOpenFolder = value;
				PTRLibSettings.Default.Save();
				RaisePropertyChanged("OpenFolderOnFinish");
			}
		}

		private bool _userExportType;
		public bool UserExportType { get { return _userExportType; } set { _userExportType = value; RaisePropertyChanged("UserExportType"); } }

		private bool _iFunctionExportType;
		public bool IFunctionExportType { get { return _iFunctionExportType; } set { _iFunctionExportType = value; RaisePropertyChanged("IFunctionExportType"); } }

		private bool _adFolderExportType;

        public bool ADFolderExportType { get { return _adFolderExportType; } set { _adFolderExportType = value; RaisePropertyChanged("ADFolderExportType"); } }

        private Action CloseAction;
        
        private UVMCommand _comExportBrowse;
        public UVMCommand comExportBrowse { get { return _comExportBrowse; } set { _comExportBrowse = value; RaisePropertyChanged("comExportBrowse"); } }

        Func<string, string, string, string> _openDialog = (type, filter, path) => { return ""; };
        public Func<string, string, string, string> OpenDialog {
            get {
                return _openDialog;
            }

            set {
                _openDialog = value;
            }
        }

    }
}
