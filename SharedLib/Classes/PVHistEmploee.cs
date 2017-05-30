using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	partial class PVHistEmploee : Notifiable, IEmploee, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }
		

		public override string ToString() {
			return "PVHistEmploee(" + PVEmploee.Name + " " + PVEmploee.Surname + "[" + PVEmploee.Id + "]) HistoricalData";
		}

		private PVStructural _departmentS;
		public string DepartmentS {
			get {
				return (_departmentS != null) ? _departmentS.Name : "Null";
			}
			set {
				_departmentS = new PVStructural() { Name = value };
			}
		}

		private PVStructural _partS;
		public string PartS {
			get {
				return (_partS != null) ? _partS.Name : "Null";
			}
			set {
				_partS = new PVStructural() { Name = value };
			}
		}
		
		public string FullName {
			get {
				return TrimmedName + " " + TrimmedSurname;
			}
		}

		private string _tName;
		public string TrimmedName {
			get {
				if (_tName == null) _tName = Name.Trim();
				return _tName;
			}
			set { _tName = value; RaisePropertyChanged("TrimmedName"); }
		}

		private string _tSurname;
		public string TrimmedSurname {
			get {
				if (_tSurname == null) _tSurname = Surname.Trim();
				return _tSurname;
			}
			set { _tSurname = value; RaisePropertyChanged("TrimmedSurname"); }
		}

		private string _tEmail;
		public string TrimmedEmail {
			get {
				if (_tEmail == null && Email != null) _tEmail = Email.Trim();
				return _tEmail;
			}
			set {
				_tEmail = value; RaisePropertyChanged("TrimmedEmail");
			}
		}

		private string _tPhone;
		public string TrimmedPhone {
			get {
				if (_tPhone == null && Phone != null) _tPhone = Phone.Trim();
				return _tPhone;
			}
			set {
				_tPhone = value; RaisePropertyChanged("TrimmedPhone");
			}
		}

		public string Profession { get { return "Unknown"; } }
        
        public string MyType { get { return "PVHistEmploee"; } }

        public int Depth { get { return 1; } set { } }
    }
}
