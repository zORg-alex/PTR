using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class ISDAVSUser : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }


		partial void OnCreated() {
			//IsPrimary = true;
			//DataType = UserDataType.ISDAVSUser;
		}

		public override string ToString() {
			return "[" + Id + "] " + Name + " " + Surname;
		}

		private string _tLogin;
		public string TrimmedLogin {
			get {
				if (_tLogin == null) _tLogin = Login.Trim();
				return _tLogin;
			}
			set { _tLogin = value; RaisePropertyChanged("TrimmedLogin"); }
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
				if (_tSurname == null && Surname != null) _tSurname = Surname.Trim();
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

		//private List<UserDataPart> _data;
		//public List<UserDataPart> Data { get { return GetData(); } }

		//public override List<UserDataPart> GetData() {
		//	var list = new List<UserDataPart>();

		//	//list.Add(new UserDataPart(UserDataPart.DataType.Id, Id.ToString()));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Name, Name));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Surname, Surname));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Email, Email));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Phone, Phone));

		//	return list;
		//}

		//public override UUser Parent {
		//	get { return UUser; }
		//	set { UUser = value; }
		//}

		public string MyType { get { return "ISDAVSUser"; } }

		public int FunctionCount { get { return IFunctionAccesses.Count; } }
        
        public int Depth { get { return 0; } set { } }
    }
}
