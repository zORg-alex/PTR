using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PTR.PTRLib {
	partial class ADFolder : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		//public static Action<ADFolder> FolderSelected = (f) => { };
		private bool _isSelected;
		public bool IsSelected {
			get { return _isSelected; }
			set {
				//FolderSelected(this);
				_isSelected = value;
			}
		}
		
		//To detect if the access was walked and is presumably deleted
		public bool walked { get; set; }

		public ADFolderAccess GetADFolderAccess(ADUser User) {
			return ADFolderAccesses.FirstOrDefault(ffa =>
				ffa.ADUser == User// && ffa.ADFolder.Id == Id // ?
			);
		}
		
		private bool _showPopUp;
		public bool ShowPopUp { get { return _showPopUp; } set { _showPopUp = value; RaisePropertyChanged("ShowPopUp"); } }

        public override string ToString() {
			return "ADFolder " + FullPath;// + ":" + Marker;
		}

		public string AccessesToString() {
			string r = "";
			foreach (var a in ADFolderAccesses) {
				r += a.ADUser.Login + a.FileSystemRights;
			}
			return r;
		}

		public string SubFoldersToString() {
			string r = "";
			foreach (var a in SubFolders) {
				r += a.Name;
			}
			return r;
		}

        int? depth;
        public int Depth {
            get {
                if (depth == null) {
                    if (ParentFolder == null) depth = 0;
                    else depth = ParentFolder.Depth + 1;
                }
                return depth.Value;
            }
            set { }
        }

	}
}
