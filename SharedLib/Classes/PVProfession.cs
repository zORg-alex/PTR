using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	using System;
	using System.Collections.Generic;

	public partial class PVProfession {

		public string NameTrimmed { get { return Name.Trim(); } }

		public override string ToString() {
			return (Name.Trim() == "" || Name == null) ? "Unknown" : Name.Trim();
		}
	}
}
