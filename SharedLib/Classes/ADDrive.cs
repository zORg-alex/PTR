using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	partial class ADDrive :Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }

		private static List<ADDrive> _adDrives;
		public static List<ADDrive> GetADDrives() {
			if (_adDrives == null) {
				var db = new PTRContext();
				_adDrives = db.ADDrives.ToList();
			}
			return _adDrives;
		}

		private List<ADFolder> _subFolders;
		public List<ADFolder> SubFolders {
			get {
				if (_subFolders == null) {
					_subFolders = ADFolders.Where((f) => f.IsRootFolder).ToList();
				}
				return _subFolders;
			}
		}

        public int Depth { get { return 0; } set { } }

        public override string ToString() {
			return "ADDrive " + Name;// + ":" + Marker;
		}
	}
}
