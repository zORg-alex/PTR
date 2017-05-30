using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class ADUser : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }

		public override string ToString() {
			return "[" + Id + "] " + Login;
		}

		public string MyType { get { return "ADUser"; } }
		public int FolderAccessesCount { get { return ADFolderAccesses.Count; } }

		private List<ADFolderAccess> _rootAccesses;
		public List<ADFolderAccess> RootAccesses {
			get {
				if(_rootAccesses == null) {
					_rootAccesses = ADFolderAccesses.Where((fa) => fa.ADFolder.IsRootFolder).ToList();
				}
				return _rootAccesses;
			}
			set { _rootAccesses = value; }
		}
		
		private List<DrivePermissions> _permissionsByDrive;
		public List<DrivePermissions> PermissionsByDrive {
			get {
				if(_permissionsByDrive == null) {
					_permissionsByDrive = new List<DrivePermissions>();
					foreach (ADDrive d in ADDrive.GetADDrives()) {
						var sra = SeparateRootAccesses.Where(s => s.ADFolder.ADDrive.Id == d.Id).ToList();
						if (sra.Count > 0) {
							_permissionsByDrive.Add(new DrivePermissions() { Drive = d, RootAccesses = sra});
						}
					}
				}
				return _permissionsByDrive; }
			set { _permissionsByDrive = value; }
		}

		private List<ADFolderAccess> _separateRootAccesses;

		public List<ADFolderAccess> SeparateRootAccesses {
			get {
				if (_separateRootAccesses == null) 
					_separateRootAccesses = ADFolderAccesses.Where(fa => 
                    (fa.ADFolder.IsRootFolder == true) ?
						true : (fa.ADFolder.ParentFolder == null) ? true :(fa.ADFolder.ParentFolder.GetADFolderAccess(this) == null) ?
							true : false
					).ToList();
				return _separateRootAccesses; }
			set { _separateRootAccesses = value; }
		}

        public int Depth { get { return 0; } set { } }
    }

	public class DrivePermissions : Notifiable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private ADDrive _drive;
		public ADDrive Drive {
			get { return _drive; }
			set { _drive = value; }
		}

		private List<ADFolderAccess> _rootAccesses;
		public List<ADFolderAccess> RootAccesses {
			get {
				return _rootAccesses;
			}
			set { _rootAccesses = value; }
		}

        public int Depth { get { return 0; } set { } }
    }
}
