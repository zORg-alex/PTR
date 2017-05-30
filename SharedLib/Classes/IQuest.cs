using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public partial class IQuest : Notifiable {

		partial void OnCreated() {
			//IsPrimary = false;
			//DataType = UserDataType.Quest;
		}

		public override string ToString() {
			return "[" + Id + "] " + Name;
		}

		//private List<UserDataPart> _data;
		//public List<UserDataPart> Data { get { return GetData(); } }

		//public override List<UserDataPart> GetData() {
		//	var list = new List<UserDataPart>();

		//	//list.Add(new UserDataPart(UserDataPart.DataType.Id, Id.ToString()));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Name, Name));
		//	list.Add(new UserDataPart(UserDataPart.DataType.Index, Index));

		//	return list;
		//}
	}
}
