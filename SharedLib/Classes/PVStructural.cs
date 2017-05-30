using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {

	public partial class PVStructural : PVNode {
		public override string ToString() {
			if (HeadDepartmentId == null) return (Id == null) ? "" : Id.TrimEnd(' ') + " " + Name.TrimEnd(' ') + " HeadId is NULL";
			return Id.TrimEnd(' ') + " " + Name.TrimEnd(' ') + " HeadId = " +  HeadDepartmentId.TrimEnd(' ');
		}

		public override PVNode ParentNode { get { return HeadDepartment; } }

		public override bool IsStructural { get { return true; } set { } }

		public override bool HaveChildren { get { return PVNodes.Count > 0; } }

		public static PVEmploee FirstPVEmploee;

		private string _filter;
		/// <summary>
		/// On FilterChange mark PVNodes to regenerate
		/// </summary>
		public override string Filter {
			get { return _filter; }
			set {
				_filter = value;
				PVNodes = null;
				//RaisePropertyChanged("Filter");
				//RaisePropertyChanged("HaveChildren");
			}
		}
		/// <summary>
		/// PVStructurals and PVEmploees list that regenerates on Filter change
		/// </summary>
		private List<PVNode> _PVNodes;
		public override List<PVNode> PVNodes {
			get {
				if (_PVNodes == null) {
					_PVNodes = new List<PVNode>();
					foreach (PVStructural S in Parts) {
						S.Filter = Filter;
						if (S.HaveChildren)
							_PVNodes.Add(S);
					}
					foreach (PVEmploee E in PVEmploeesInPart) {
						if (E.UUser.NormalizeFullname.Contains(Filter.ToLower()) && E.UUser.Hide == false) {
							if (FirstPVEmploee == null)
								FirstPVEmploee = E;
							_PVNodes.Add(E);
						}
					}
					RaisePropertyChanged("PVNodes");
				}
				return _PVNodes;
			}
			set {
				_PVNodes = value;
				RaisePropertyChanged("PVNodes");
			}
		}
	}
}
