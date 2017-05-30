using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class IFunction : Notifiable {

		//partial void OnCreated() {
			//IsPrimary = false;
			//DataType = UserDataType.Function;
		//}

		public override string ToString() {
			return "[" + Id + "] " + Name;
		}

		//private List<UserDataPart> _data;
		//public List<UserDataPart> Data { get { return GetData(); } }

		//public override List<UserDataPart> GetData() {
		//	var list = new List<UserDataPart>();

		//	//list.Add(new UserDataPart(UserDataPart.DataType.Id, Id.ToString()));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Name, Name));

		//	return list;
		//}


		public string NameTrimmed { get { return Name.TrimEnd(' '); } }
		public string DescriptionTrimmed { get { return Description.TrimEnd(' '); } }
		public int AccessesCount { get { return IFunctionAccesses.Count; } }
	}
}
