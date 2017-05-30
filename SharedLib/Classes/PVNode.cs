using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	public class PVNode : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }
		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }

		public virtual bool IsStructural { get; set; }

		public virtual bool HaveChildren { get; }

		public virtual PVNode ParentNode { get; }

		public virtual List<PVNode> PVNodes { get; set; }

		public virtual string Filter { get; set; }
        
        int? depth;
        public int Depth {
            get {
                if (depth == null) {
                    if (ParentNode == null) depth = 0;
                    else depth = ParentNode.Depth + 1;
                }
                return depth.Value;
            }
            set { }
        }
    }
}
