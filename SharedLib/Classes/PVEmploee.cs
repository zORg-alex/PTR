using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class PVEmploee : PVNode, IEmploee {

		public override PVNode ParentNode { get { return Part; } }

		public override bool IsStructural { get { return false; } set { } }

		public override bool HaveChildren { get { return false; } }

		private List<PVNode> _PVNodes = new List<PVNode>();
		public override List<PVNode> PVNodes { get {
				return _PVNodes;
			}
			set { }
		}

		public override string ToString() {
			return "PVEmploee(" + Name + " " + Surname + ")";
		}

		private string _normalizedName;
		public string NormalizedName {
			get {
				if (_normalizedName == null) _normalizedName = LatinToASCIIConverter.Convert(TrimmedName);
				return _normalizedName;
			}
			set { _normalizedName = value; }
		}

		private string _normalizedSurname;
		public string NormalizedSurname {
			get {
				if (_normalizedSurname == null) _normalizedSurname = LatinToASCIIConverter.Convert(_normalizedSurname);
				return _normalizedSurname;
			}
			set { _normalizedSurname = value; }
		}

		public string FullName {
			get {
				return TrimmedName + " " + TrimmedSurname;
			}
		}
		
		private string _normalizeFullname;
		public string NormalizeFullname {
			get {
				if (_normalizeFullname == null) {
					_normalizeFullname = LatinToASCIIConverter.Convert(FullName);
				}
				return _normalizeFullname;
			}
			set { _normalizeFullname = value; }
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
		private string _tLocalPhone;
		public string TrimmedLocalPhone {
			get {
				if (_tLocalPhone == null && LocalPhone != null) _tLocalPhone = LocalPhone.Trim();
				return _tLocalPhone;
			}
			set {
				_tLocalPhone = value; RaisePropertyChanged("TrimmedLocalPhone");
			}
		}
		
		public string DepartmentS {
			get {
				return (Department != null) ? Department.Name : "Null";
			}
			set {
				Department = new PVStructural() { Name = value};
			}
		}
		
		public string PartS {
			get {
				return (Part != null) ? Part.Name : "Null";
			}
			set {
				Part = new PVStructural() { Name = value };
			}
		}

		public string Profession {
			get {
                if (PVProfession == null) return "";
				if (PVProfession.Name == "" || PVProfession.Name == null) return "unknown";
				return PVProfession.Name;
			}
		}

		private string _filter;
		public override string Filter { get { return _filter; } set { _filter = value; } }

        public string MyType { get { return "PVEmploee"; } }
    }
}
