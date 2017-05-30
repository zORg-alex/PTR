using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public interface IEmploee {
		string Name { get; }
		string Surname { get; }
		string Email { get; }
		string DepartmentS { get; }
		string PartS { get; }
		string Profession { get; }
		string Phone { get; }
		string LocalPhone { get; }
		bool Status { get; }
		DateTime? From { get; }
		DateTime? To { get; }
	}
}
