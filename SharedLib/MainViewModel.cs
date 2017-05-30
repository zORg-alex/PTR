using OfficeOpenXml;
using PTR.PTRLib;
using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PTR.PTRLib {
	/// <summary>
	/// Viewmodel for MainWindow
	/// </summary>
	/// <remarks>
	/// ViewModel class is no longere necessary since ModernUI have all basic window behaviour.
	/// It was used to mimic usual window behaviour like minimizind and restoring a normal or maximized window,
	/// set right window border while maximized, so the window would not be extended beyound the screen.
	/// </remarks>
	public class MainViewModel : Notifiable {

		static PTRContext db;
		public static MainViewModel instance;
		public Window Window;
		public bool Execute;

        private float _inactiveWindowDesaturation;//Just for designer sake, in runtime it should be 0.5f
        public float InactiveWindowDesaturation { get { return _inactiveWindowDesaturation; } set { _inactiveWindowDesaturation = value; RaisePropertyChanged("InactiveWindowDesaturation"); } }

        public MainViewModel() : this(false) { }

		public MainViewModel(bool Execute) : base() {
            this.Execute = Execute;
			instance = this;
            //if (OpenExportWindow != null) Execute = true;
            if (Execute) {
                InactiveWindowDesaturation = .5f;
                // Working section
                db = new PTRContext();
                db.Database.Log = s => Console.WriteLine(s);
                UUsers = db.UUsers.Include("PVEmploees.department").Include("PVEmploees.part").OrderBy(a => a.Name).ToList();
                IFunctions = db.IFunctions.OrderBy(a => a.Name).ToList();
                ADDriveList = db.ADDrives.OrderBy(a => a.Name).ToList();
                ReportList = db.ImportReports.Where(r => r.Hide == 0).ToList();

                PVStructurals = db.PVStructurals.Include("HeadDepartment").Include("PVEmploeesInPart").ToList();
                PVStructuralRoot.Add(PVStructurals.Find(s => s.Code == "0"));

                SetupMainViewModelCommands();
                CurrentView = Views.UserView;

                ExportViewModel = new ExportViewModel();
                IsExportVisible = false;
            } else {
                // Test section
                UUsers = TestData.UUsers;
                IFunctions = TestData.IFunctions;
                ADDriveList = TestData.ADDrives;
                ReportList = TestData.ImportReports.Where(r => r.Hide == 0).ToList();
                SelectedADFolder = TestData.ADFolder;
                PVStructuralRoot = TestData.PVStructurals;
            }
			//Common Section
			ReportsVisibility = Visibility.Collapsed;
			SelectedIFunction = FilteredIFunctionList.FirstOrDefault();
			UUserFilter = "";
			IFunctionFilter = "";
			MergeUUserFilter = "";
			SelectedMergeUUser = FilteredMergeUUserList.FirstOrDefault();
			SelectedMergeUUser2 = SelectedMergeUUser;
            SelectedIFunction = FilteredIFunctionList.FirstOrDefault();

		}

		public void SaveToDB() {
			if (Execute) db.SaveChanges();
		}

		private bool _isMenuVisible;
		public bool IsMenuVisible {
            get { return _isMenuVisible; }
            set {
                _isMenuVisible = value;
                IsMergeViewButtonVisible = IsMergeViewVisible || IsMenuVisible;
                RaisePropertyChanged("IsMenuVisible");
            }
        }

		public enum Views { None, UserView, FunctionView, MergeView, FolderView }
		private Views _currentView;
		public Views CurrentView {
			get {
				return _currentView;
			}
			set {
				if (_currentView != value) {
                    IsMenuVisible = false;
                    if (value == Views.None) {
						IsUserViewVisible = false;
						IsFuncViewVisible = false;
						IsMergeViewVisible = false;
						IsFolderViewVisible = false;
						_currentView = value;
						RaisePropertyChanged("CurrentView");
					} else if (value == Views.UserView) {
						IsUserViewVisible = true;
						IsFuncViewVisible = false;
						IsMergeViewVisible = false;
						IsFolderViewVisible = false;
						_currentView = value;
						RaisePropertyChanged("CurrentView");
					} else if (value == Views.FunctionView) {
						IsUserViewVisible = false;
						IsFuncViewVisible = true;
						IsMergeViewVisible = false;
						IsFolderViewVisible = false;
						_currentView = value;
						RaisePropertyChanged("CurrentView");
					} else if (value == Views.MergeView) {
						IsUserViewVisible = false;
						IsFuncViewVisible = false;
						IsMergeViewVisible = true;
						IsFolderViewVisible = false;
						_currentView = value;
						RaisePropertyChanged("CurrentView");
					} else if (value == Views.FolderView) {
						IsUserViewVisible = false;
						IsFuncViewVisible = false;
						IsMergeViewVisible = false;
						IsFolderViewVisible = true;
						_currentView = value;
						RaisePropertyChanged("CurrentView");
					}
				}
			}
		}

		private bool _isUserViewVisible;
		public bool IsUserViewVisible {
			get { return _isUserViewVisible; }
			set {
				if (value != _isUserViewVisible) {
					_isUserViewVisible = value;
					CurrentView = Views.UserView;
					RaisePropertyChanged("IsUserViewVisible");
				}
			}
		}

		private bool _isFuncViewViewVisible;
		public bool IsFuncViewVisible {
			get { return _isFuncViewViewVisible; }
			set {
				if (value != _isFuncViewViewVisible) {
					_isFuncViewViewVisible = value;
					CurrentView = Views.FunctionView;
					RaisePropertyChanged("IsFuncViewVisible");
				}
			}
		}

		private bool _isMergeViewVisible;
		public bool IsMergeViewVisible {
			get { return _isMergeViewVisible; }
			set {
				if (value != _isMergeViewVisible) {
					_isMergeViewVisible = value;
                    IsMergeViewButtonVisible = IsMergeViewVisible || IsMenuVisible;
					CurrentView = Views.MergeView;
					RaisePropertyChanged("IsMergeViewVisible");
				}
			}
        }
        public Visibility MergeViewVisibility { get { return (_isMergeViewVisible)?Visibility.Visible:Visibility.Collapsed; } }
        
        private bool _isMergeViewButtonVisible;
        public bool IsMergeViewButtonVisible { get { return _isMergeViewButtonVisible; } set { _isMergeViewButtonVisible = value; RaisePropertyChanged("IsMergeViewButtonVisible"); } }

		private bool _isFolderViewVisible;
		public bool IsFolderViewVisible {
			get { return _isFolderViewVisible; }
			set {
				if (value != _isFolderViewVisible) {
					_isFolderViewVisible = value;
					CurrentView = Views.FolderView;
					RaisePropertyChanged("IsFolderViewVisible");
				}
			}
		}

        private bool _isExportVisible;
        public bool IsExportVisible { get { return _isExportVisible; } set { _isExportVisible = value; RaisePropertyChanged("IsExportVisible"); } }
        
        private ExportViewModel _exportViewModel;
        public ExportViewModel ExportViewModel { get { return _exportViewModel; } set { _exportViewModel = value; RaisePropertyChanged("ExportViewModel"); } }
        
		#region Commands
		public ICommand SetView_UserView { get; private set; }
		public ICommand SetView_MergeView { get; private set; }
		public ICommand SetView_FuncView { get; private set; }
		public ICommand SetView_FolderView { get; private set; }
        public ICommand comToggleReports { get; private set; }
        public ICommand comToggleMenu { get; private set; }
        public ICommand comHideReports { get; private set; }
		public ICommand comReportClicked { get; private set; }
		public ICommand comMergeViewTransfer { get; private set; }
		public ICommand comClearFilter { get; private set; }
		public ICommand comToggleUser { get; private set; }
        public ICommand comExport { get; private set; }
        public ICommand comExportClose { get; private set; }

        /// <summary>
        /// Sets up all commands in one place. Should put all Command initializations here.
        /// </summary>
        private void SetupMainViewModelCommands() {
			//SetupMergeViewCommands();

			comToggleReports = new UVMCommand((p) => {
				ReportsVisibility = (ReportsVisibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
			});

			comHideReports = new UVMCommand((p) => {
				ReportsVisibility = Visibility.Collapsed;
			}, true);

			comToggleUser = new UVMCommand((p) => {
				SelectedMergeUUser.Hide = !SelectedMergeUUser.Hide;
				UUserFilter = UUserFilter;
			});

			SetView_UserView = new UVMCommand((p) => {
				CurrentView = Views.UserView;
                IsMenuVisible = false;
            }, (p) => {
				return (CurrentView != Views.UserView);
			});

			SetView_FuncView = new UVMCommand((p) => {
				CurrentView = Views.FunctionView;
                IsMenuVisible = false;
            }, (p) => {
				return (CurrentView != Views.FunctionView);
			});

			SetView_MergeView = new UVMCommand((p) => {
				CurrentView = Views.MergeView;
                IsMenuVisible = false;
			}, (p) => {
				return (CurrentView != Views.MergeView);
			});

			SetView_FolderView = new UVMCommand((p) => {
				CurrentView = Views.FolderView;
                IsMenuVisible = false;
            }, (p) => {
				return (CurrentView != Views.FolderView);
			});

            comToggleMenu = new UVMCommand((p) => {
                IsMenuVisible = !IsMenuVisible;
            });

			comClearFilter = new UVMCommand((p) => {
				switch (CurrentView) {
					case Views.UserView:
						UUserFilter = "";
						break;
					case Views.FunctionView:
						IFunctionFilter = "";
						break;
					case Views.MergeView:
						MergeUUserFilter = "";
						break;
					case Views.FolderView:
						break;
					default:
						break;
				}
			}, true);

			IQuestAccess.Commands.Add(new UVMCommand("comExpandInUserView", (p) => {
				var iQA = (p as IQuestAccess);
				iQA.IsSelected = true;
				CurrentView = Views.UserView;
				SelectedUUser = iQA.IFunctionAccess.ISDAVSUser.UUser;
				iQA.IFunctionAccess.ISDAVSUser.IsExpanded = true;
				iQA.IFunctionAccess.IsExpanded = true;
				iQA.IsExpanded = true;
			}));

			IQuestAccess.Commands.Add(new UVMCommand("comExpandInIFunctionView", (p) => {
				var iQA = (p as IQuestAccess);
				iQA.IsSelected = true;
				CurrentView = Views.FunctionView;
				SelectedIFunction = iQA.IFunctionAccess.IFunction;
				//SelectedFuncAccess = iQA.IFunctionAccess;
				iQA.IFunctionAccess.IsExpanded = true;
				iQA.IsExpanded = true;
			}));

			IFunctionAccess.Commands.Add(new UVMCommand("comExpandInUserView", (p) => {
				var iFA = (p as IFunctionAccess);
				iFA.IsSelected = true;
				if (iFA == null) return;
				CurrentView = Views.UserView;
				SelectedUUser = iFA.ISDAVSUser.UUser;
				iFA.ISDAVSUser.IsExpanded = true;
				iFA.IsExpanded = true;
			}));

			IFunctionAccess.Commands.Add(new UVMCommand("comExpandInIFunctionView", (p) => {
				var iFA = (p as IFunctionAccess);
				iFA.IsSelected = true;
				CurrentView = Views.FunctionView;
				SelectedIFunction = iFA.IFunction;
				//SelectedFuncAccess = iFA;
				iFA.IsExpanded = true;
			}));

			ImportReport.Commands.Add(new UVMCommand("comOnClick", (p) => {
				ReportClicked(p as ImportReport);
			}));

			ImportReport.Commands.Add(new UVMCommand("comOnClickHide", (p) => {
				ReportViewAndHide(p as ImportReport);
			}));

			//A trick to walk around TreeView obscured SelectedItem thing
			//Is used in ADFoldersTree in FolderView, folder stops being selected on commenting next line
			//May be a good thing to improve this to make code more readable
			//ADFolder.FolderSelected = (f) => SelectedADFolder = f;

			// Flippin Hate this piece of code
			// This hated thing finds a UUser->ADUser->PermissionsByDrive->RootAccesses->...->ADFolderAccess(by _ADFolderAccess.ADFolder.FullPath)
			// selects it and expands all parents
			ADFolderAccess.Commands.Add(new UVMCommand("comShowInUserView", (p) => {
				var _ADFolderAccess = p as ADFolderAccess;
				CurrentView = Views.UserView;
				SelectedUUser = UUsers.FirstOrDefault(uu => uu.Id == _ADFolderAccess.ADUser.UUser.Id);
				var _ADUser = SelectedUUser.ADUsers.FirstOrDefault();
				_ADUser.IsExpanded = true;
				var _DrivePermission = _ADUser.PermissionsByDrive.Find(d => d.Drive.Id == _ADFolderAccess.ADFolder.DriveId);
				_DrivePermission.IsExpanded = true;
				ADFolderAccess result = null;
				foreach (var ra in _DrivePermission.RootAccesses) {
					result = ADFolderAccessInwardRecursion(ra, _ADFolderAccess.ADFolder.FullPath);
					if (result != null) {
						result.IsExpanded = true;
						AdFolderAccessExpandRecursion(result);
						break;
					}
				}
			}));

			// Searches and Expands FolderAccesses to _ADFolderAccess
			ADFolderAccess.Commands.Add(new UVMCommand("comShowInFolderView", (p) => {
				var _ADFolderAccess = p as ADFolderAccess;
				CurrentView = Views.FolderView;
				SelectedADDrive = ADDriveList.Find(d => d.Id == _ADFolderAccess.ADFolder.ADDrive.Id);
				var _ADFolder = SelectedADDrive.ADFolders.Single(f => f.Id == _ADFolderAccess.ADFolder.Id);
				ADFolderExpandRecursion(_ADFolder);
				_ADFolder.IsSelected = true;
			}));

			comMergeViewTransfer = new UVMCommand((p) => {
				//Transfer Selected Data in MergeView
				switch (SelectedMergeUUserData.GetType().BaseType.Name) {
					case "ISDAVSUser":
						if (LeftToRightMergeDirection) {
							SelectedMergeUUser.ISDAVSUsers.Remove(SelectedMergeUUserData as ISDAVSUser);
							SelectedMergeUUser2.ISDAVSUsers.Add(SelectedMergeUUserData as ISDAVSUser);
						} else {
							SelectedMergeUUser2.ISDAVSUsers.Remove(SelectedMergeUUserData as ISDAVSUser);
							SelectedMergeUUser.ISDAVSUsers.Add(SelectedMergeUUserData as ISDAVSUser);
						}
						break;
					case "ADUser":
						if (LeftToRightMergeDirection) {
							SelectedMergeUUser.ADUsers.Remove(SelectedMergeUUserData as ADUser);
							SelectedMergeUUser2.ADUsers.Add(SelectedMergeUUserData as ADUser);
						} else {
							SelectedMergeUUser2.ADUsers.Remove(SelectedMergeUUserData as ADUser);
							SelectedMergeUUser.ADUsers.Add(SelectedMergeUUserData as ADUser);
						}
						break;
					case "PVEmploee":
						if (LeftToRightMergeDirection) {
							SelectedMergeUUser.PVEmploees.Remove(SelectedMergeUUserData as PVEmploee);
							SelectedMergeUUser2.PVEmploees.Add(SelectedMergeUUserData as PVEmploee);
						} else {
							SelectedMergeUUser2.PVEmploees.Remove(SelectedMergeUUserData as PVEmploee);
							SelectedMergeUUser.PVEmploees.Add(SelectedMergeUUserData as PVEmploee);
						}
						break;
					default:
						break;
				}
				SelectedMergeUUser.Evaluate();
				SelectedMergeUUser2.Evaluate();
			}, (p) => {
				//Check whether Left User has a data selected
				if (SelectedMergeUUserData != null) return true;
				return false;
			});

            comExport = new UVMCommand((par) => {
                if (CurrentView == Views.FolderView) { SelectedObject = SelectedADFolder; }
                if (CurrentView == Views.FunctionView) { SelectedObject = SelectedIFunction; }
                ExportViewModel = new ExportViewModel(SelectedObject, () => { IsExportVisible = false; });
                IsExportVisible = true;
            });

            comExportClose = new UVMCommand((par) => {
                IsExportVisible = false;
            });
        }

		public void SelectRecordInItsView(object Object, Views View) {
			if (Object.GetType().BaseType.Name == "IFunction") {
				//TODO ?
			}
		}
		#endregion

		private object _selectedObject;
		public object SelectedObject {
			get { return _selectedObject; }
			set {
				if (SelectedObject != value) {
					_selectedObject = value;
					if (value as PVEmploee != null) SelectedUUser = (value as PVEmploee).UUser;
					RaisePropertyChanged("SelectedObject");
				}
			}
		}

		//User View Properties
		private List<UUser> _uUsers = new List<UUser>();
		/// <summary>
		/// Stores UUser query
		/// </summary>
		public List<UUser> UUsers { get { return _uUsers; } set { _uUsers = value; RaisePropertyChanged("UUsers"); } }

		private List<UUser> _filteredUUserLIst = new List<UUser>();
		public List<UUser> FilteredUUserList { get { return _filteredUUserLIst; } set { _filteredUUserLIst = value; RaisePropertyChanged("FilteredUUserList"); } }

		private string _uUserFilter;
		public string UUserFilter {
			get { return _uUserFilter; }
			set {
				_uUserFilter = value;
				FilteredUUserList = UUsers.FindAll(s =>
					s.NormalizeFullname.Contains(UUserFilter.ToLower()) && s.Hide == false
					);
				PVStructural.FirstPVEmploee = null;
				PVStructuralRoot.FirstOrDefault().Filter = _uUserFilter;
				List<PVNode> root = new List<PVNode>();
				root.Add(PVStructuralRoot.FirstOrDefault());
				FilteredPVNodes = root;
				PVStructuralRoot.FirstOrDefault().PVNodes.Count();//Trigger Filtering
				SelectedPVEmploee = PVStructural.FirstPVEmploee;
				RaisePropertyChanged("UUserFilter");
			}
		}

		private List<PVStructural> _pVStructurals = new List<PVStructural>();
		/// <summary>
		/// Stores PVStructurals query
		/// </summary>
		public List<PVStructural> PVStructurals { get { return _pVStructurals; } set { _pVStructurals = value; RaisePropertyChanged("PVStructurals"); } }

		private List<PVStructural> _pVStructuralRoot = new List<PVStructural>();
		public List<PVStructural> PVStructuralRoot { get { return _pVStructuralRoot; } set { _pVStructuralRoot = value; RaisePropertyChanged("PVStructuralRoot"); } }

		private List<PVNode> _filteredPVNodes = new List<PVNode>();
		public List<PVNode> FilteredPVNodes { get { return _filteredPVNodes; } set { _filteredPVNodes = value; RaisePropertyChanged("FilteredPVNodes"); } }

		private UUser _selectedUUser;
		public UUser SelectedUUser {
			get { return _selectedUUser; }
			set {
				if (value != null && _selectedUUser != value) {
					//Deselect old ones in UI first. A fix for Treeview leaving old selection in a different branch
					if (SelectedUUser != null) {
						if (SelectedUUser.PVEmploees.Count > 0) SelectedUUser.PVEmploees.FirstOrDefault().IsSelected = false;
						SelectedUUser.IsSelected = false;
					}
					_selectedUUser = value;
					if (SelectedUUser.PVEmploees.Count > 0) SelectedUUser.PVEmploees.FirstOrDefault().IsSelected = true;
					SelectedUUser.IsSelected = true;
					SelectedPVEmploee = value.PVEmploees.FirstOrDefault();
					RaisePropertyChanged("SelectedUUser");
					SelectedObject = value;
				}
			}
		}

		public PVEmploee SelectedPVEmploee {
			get { return SelectedUUser.PVEmploees.FirstOrDefault(); }
			set {
				if (value != null) {
					//Deselect old ones in UI first. A fix for Treeview
					if (SelectedUUser != value.UUser) {
						if (SelectedUUser != null) {
							if (SelectedUUser.PVEmploees.Count > 0) SelectedUUser.PVEmploees.FirstOrDefault().IsSelected = false;
							SelectedUUser.IsSelected = false;
						}
						SelectedUUser = value.UUser;
						value.IsExpanded = true;
						PVNodeRecursiveExpand(value);
						SelectedObject = value;
						RaisePropertyChanged("SelectedPVEmploee");
					}
				}
			}
		}

		private void PVNodeRecursiveExpand(PVNode N) {
			if (N == null) return;
			N.IsExpanded = true;
			PVNodeRecursiveExpand(N.ParentNode);
		}


		private object _selectedUserViewDetail;
		public object SelectedUserViewDetail { get { return _selectedUserViewDetail; } set { _selectedUserViewDetail = value; RaisePropertyChanged("SelectedUserViewDetail"); } }

		private bool _uUserTreeBusy = false;
		public bool UUserTreeBusy { get { return _uUserTreeBusy; } set { _uUserTreeBusy = value; RaisePropertyChanged("UUserTreeBusy"); } }

		//Function View Properties
		private List<IFunction> _IFunctions = new List<IFunction>();
		/// <summary>
		/// Stores IFunctions query
		/// </summary>
		public List<IFunction> IFunctions { get { return _IFunctions; } set { _IFunctions = value; RaisePropertyChanged("IFunctions"); } }

		private List<IFunction> _filteredIFunctionList = new List<IFunction>();
		public List<IFunction> FilteredIFunctionList { get { return _filteredIFunctionList; } set { _filteredIFunctionList = value; RaisePropertyChanged("FilteredIFunctionList"); } }

		private string _IFunctionFilter;
		public string IFunctionFilter {
			get { return _IFunctionFilter; }
			set {
				_IFunctionFilter = value;
				var list = IFunctions.FindAll(s =>
					s.Name.ToLower().Contains(IFunctionFilter.ToLower())
					);
				FilteredIFunctionList = list;
				RaisePropertyChanged("IFunctionFilter");
			}
		}

		private IFunction _selectedIFunction;
		public IFunction SelectedIFunction {
			get { return _selectedIFunction; }
			set {
				if (value != null) {
					IFunction r;
					if (Execute)
						r = db.IFunctions.FirstOrDefault(f => f.Id == value.Id);
					else
						r = IFunctions.FirstOrDefault(f => f.Id == value.Id);
					RaisePropertyChanged("SelectedIFunction");
					//SelectedObject = value;
					_selectedIFunction = r;
				}
			}
		}

		private bool _IFunctionTreeBusy;
		public bool IFunctionTreeBusy { get { return _IFunctionTreeBusy; } set { _IFunctionTreeBusy = value; RaisePropertyChanged("IFunctionTreeBusy"); } }

		//Folder View

		private List<ADDrive> _ADDriveList = new List<ADDrive>();
		/// <summary>
		/// Stores ADDrives query
		/// </summary>
		public List<ADDrive> ADDriveList { get { return _ADDriveList; } set { _ADDriveList = value; RaisePropertyChanged("ADDriveList"); } }

		private ADDrive _selectedADDrive;
		public ADDrive SelectedADDrive { get { return _selectedADDrive; } set { _selectedADDrive = value; RaisePropertyChanged("SelectedADDrive"); } }

		private ADFolder _selectedADFolder;
		public ADFolder SelectedADFolder { get { return _selectedADFolder; } set { _selectedADFolder = value; RaisePropertyChanged("SelectedADFolder"); } }

		//Merge View

		private List<UUser> _filteredMergeUUserLIst = new List<UUser>();
		public List<UUser> FilteredMergeUUserList { get { return _filteredMergeUUserLIst; } set { _filteredMergeUUserLIst = value; RaisePropertyChanged("FilteredMergeUUserList"); } }

		private string _mergeUUserFilter;
		public string MergeUUserFilter {
			get { return _mergeUUserFilter; }
			set {
				_mergeUUserFilter = value;
				var list = UUsers.FindAll(s =>
					s.NormalizeFullname.Contains(MergeUUserFilter.ToLower())
					);
				FilteredMergeUUserList = list;
				RaisePropertyChanged("MergeUUserFilter");
			}
		}

		private UUser _selectedMergeUUser;
		public UUser SelectedMergeUUser { get { return _selectedMergeUUser; } set { _selectedMergeUUser = value; RaisePropertyChanged("SelectedMergeUUser"); } }

		private object _selectedMergeUUserData;
		public object SelectedMergeUUserData {
			get { return _selectedMergeUUserData; }
			set {
				if (((ISelectable)_selectedMergeUUserData) != null) {
					((ISelectable)_selectedMergeUUserData).IsSelected = false;
				}// catch (Exception) { }
				_selectedMergeUUserData = value;
				RaisePropertyChanged("SelectedMergeUUserData");
				if (SelectedMergeUUser != null) if (SelectedMergeUUser.LinkedUsers.Contains(value)) LeftToRightMergeDirection = true;
				else LeftToRightMergeDirection = false;
			}
		}

		private UUser _selectedMergeUUser2;
		public UUser SelectedMergeUUser2 { get { return _selectedMergeUUser2; } set { _selectedMergeUUser2 = value; RaisePropertyChanged("SelectedMergeUUser2"); } }

		private bool _leftToRightMergeDirection;
		public bool LeftToRightMergeDirection { get { return _leftToRightMergeDirection; } set { _leftToRightMergeDirection = value; RaisePropertyChanged("LeftToRightMergeDirection"); } }
		//Reports Properties

		private List<ImportReport> _reportList;
		/// <summary>
		/// Stores ImportReports query
		/// </summary>
		public List<ImportReport> ReportList { get { return _reportList; } set { _reportList = value; RaisePropertyChanged("ReportList"); } }

		private Visibility _reportsVisibility;
		public Visibility ReportsVisibility { get { return _reportsVisibility; } set { _reportsVisibility = value; RaisePropertyChanged("ReportsVisibility"); } }

		public void ReportClicked(ImportReport Report) {
			if (Report == null) return;
			if (Report.Table == "ISDAVSUsers") {
				CurrentView = Views.UserView;
				var u = db.ISDAVSUsers.FirstOrDefault(f => f.Id == Report.RecordId);
				if (u == null) return;
				SelectedUUser = u.UUser;
				u.IsExpanded = true;
			} else if (Report.Table == "ADUser") {
				CurrentView = Views.UserView;
				var u = db.ADUsers.FirstOrDefault(f => f.Id == Report.RecordId);
				if (u == null) return;
				SelectedUUser = u.UUser;
				u.IsExpanded = true;
			} else if (Report.Table == "IFunctionAccesess") {
				CurrentView = Views.UserView;
				var fa = db.IFunctionAccesses.FirstOrDefault(f => f.Id == Report.RecordId); ;
				if (fa == null) return;
				SelectedUUser = fa.ISDAVSUser.UUser;
				fa.ISDAVSUser.IsExpanded = true;
				fa.IsExpanded = true;
			} else if (Report.Table == "IQuestAccesess") {
				CurrentView = Views.UserView;
				var q = db.IQuestAccesses.FirstOrDefault(f => f.Id == Report.RecordId); ;
				if (q == null) return;
				SelectedUUser = q.IFunctionAccess.ISDAVSUser.UUser;
				q.IFunctionAccess.ISDAVSUser.IsExpanded = true;
				q.IFunctionAccess.IsExpanded = true;
			} else if (Report.Table == "IQuestSet") {
				CurrentView = Views.UserView;
				var q = db.IQuests.FirstOrDefault(f => f.Id == Report.RecordId); ;
				if (q == null) return;
				//? Do what ?
				// No supported view yet
				Report.Hide = 1;
				return;
			} else if (Report.Table == "PVEmploees") {
				CurrentView = Views.UserView;
				var e = db.PVEmploees.FirstOrDefault(f => f.Id == Report.RecordId); ;
				if (e == null) return;
				SelectedUUser = e.UUser;
				e.IsExpanded = true;
			} else if (Report.Table == "IFunction") {
				CurrentView = Views.FunctionView;
				SelectedIFunction = db.IFunctions.FirstOrDefault(o => o.Id == Report.RecordId); ;
			}
			ReportsVisibility = Visibility.Collapsed;
			return;
		}

		public void ReportViewAndHide(ImportReport importReport) {
			//importReport.Hide = 1;
			SaveToDB();
			ReportList.Remove(importReport);
			ReportClicked(importReport);
		}

		/// <summary>
		/// Recursively expands all parent ADFolders
		/// </summary>
		/// <param name="f"></param>
		void ADFolderExpandRecursion(ADFolder f) {
			if (f == null) return;
			f.IsExpanded = true;
			ADFolderExpandRecursion(f.ParentFolder);
		}

		/// <summary>
		/// Recursively searches for an Access with a given folders FullPath
		/// </summary>
		/// <param name="Current"></param>
		/// <param name="FullPath"></param>
		/// <returns></returns>
		ADFolderAccess ADFolderAccessInwardRecursion(ADFolderAccess Current, string FullPath) {
			if (Current == null) return null;
			if (FullPath == Current.ADFolder.FullPath) {
				Current.IsSelected = true;
				return Current;
			}
			ADFolderAccess r;
			foreach (ADFolderAccess a in Current.SubFolderAccesses) {
				r = ADFolderAccessInwardRecursion(a, FullPath);
				if (r != null) {
					r.IsExpanded = true;
					return r;
				}
			}
			return null;
		}

		/// <summary>
		/// Expands all parent Accesses
		/// </summary>
		/// <param name="Current"></param>
		void AdFolderAccessExpandRecursion(ADFolderAccess Current) {
			if (Current == null) return;
			Current.IsExpanded = true;
			AdFolderAccessExpandRecursion(Current.ParentFolderAccess);
		}
	}
}