using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PTR.PTRLib {
	partial class ADFolderAccess : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }

		private static UVMCommandHost _commands;
		public static UVMCommandHost Commands {
			get {
				if (_commands == null) _commands = new UVMCommandHost();
				return _commands;
			}
			set { _commands = value; }
		}

		public FileSystemRights FileSystemRights {
			get { return (FileSystemRights)FileSystemRights_; }
			set { FileSystemRights_ = (int) value; }
		}

		private string _displayName;
		public string DisplayName {
			get {
				if (_displayName == null) {
					_displayName = ADFolder.Name;
					if (ADFolder.ParentFolderId.HasValue) {
						if (ADFolder.ParentFolder.GetADFolderAccess(ADUser) == null)
							_displayName = ADFolder.FullPath;
					}
				}
				return _displayName;
			}
			set { _displayName = value; }
		}

		public override bool Equals(object obj) {
			ADFolderAccess O = obj as ADFolderAccess;
			if (O == null) return false;
			if (this.ADFolder == O.ADFolder &&
				this.ADUser == O.ADUser &&
				this.FileSystemRights_ == O.FileSystemRights_ &&
				this.From == O.From &&
				this.To == O.To)
				return true;
			return false;
		}

		//To detect if the access was walked and is presumably deleted
		public bool walked { get; set; }

		public bool Status { get { return (To == null) ? true : false; } }

		public override string ToString() {
			return "ADFolderAccess " + ADFolder.FullPath + " to " + ((ADUser != null) ? ADUser.Login : "No ADUser") + " - " + FileSystemRights.ToString();
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

        int? depth;
        public int Depth {
            get {
                if (depth == null) {
                    if (ParentFolderAccess == null) depth = 0;
                    else depth = ParentFolderAccess.Depth + 1;
                }
                return depth.Value;
            }
            set { }
        }
    }
}
