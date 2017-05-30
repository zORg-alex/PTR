using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class UUser : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }


		partial void OnCreated() {
		}

		public void Evaluate() {
			var list = new List<object>();
			foreach (PVEmploee e in PVEmploees) {
				list.Add(e);
			}
			foreach (ISDAVSUser u in ISDAVSUsers) {
				list.Add(u);
			}
			foreach (ADUser u in ADUsers) {
				list.Add(u);
            }
            LinkedUsers = list;
		}

		public bool Hide { get { return hide; } set { hide = value; RaisePropertyChanged("Hide"); } }

		private List<object> _linkedUsers;

		public List<object> LinkedUsers {
			get {
                if (_linkedUsers == null) Evaluate();
                return _linkedUsers;
            }
			set { _linkedUsers = value; RaisePropertyChanged("LinkedUsers"); }
		}

		private string _normalizeFullname;
		public string NormalizeFullname {
			get {
				if (_normalizeFullname == null || _normalizeFullname == "") {
					_normalizeFullname = LatinToASCIIConverter.Convert(FullName.ToLower());
					if (NormalizedFullName != _normalizeFullname) NormalizedFullName = _normalizeFullname;
				}
				return _normalizeFullname;
			}
			set { _normalizeFullname = value; }
		}

		private string _fullName;
		public string FullName {
			get {
				if (_fullName == null || _fullName == "") {
                    _fullName = (Name != null) ? Name.TrimEnd() + " " : "";
                    _fullName += ((Surname != null) ? Surname.TrimEnd() : "");
				}
				return _fullName;
			}
			set { _fullName = value; }
		}

		public override string ToString() {
			return "UUser " + FullName;
		}

        public int Depth { get { return 0; } set { } }
    }
}
