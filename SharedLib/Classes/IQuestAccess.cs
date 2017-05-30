using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PTR.PTRLib {
	public partial class IQuestAccess : Notifiable, ISelectable {
		private bool _isExpanded;
		public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }

		private bool _isSelected;
		public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged("IsSelected"); } }


		private static UVMCommandHost _commands;
		public static UVMCommandHost Commands {
			get {
				if (_commands == null) _commands = new UVMCommandHost();
				return _commands;
			}
			set { _commands = value; }
		}

        //	public ICommand comExpandInView { get; private set; }

        //	public IQuestAccess() {
        //		comExpandInView = new UVMCommand((p) => {

        //			if ((p as string) == MainViewModel.Views.UserView.ToString())
        //				MainViewModel.instance.ExpandToIFunctionAccessInUserView(this);
        //			else
        //				MainViewModel.instance.ExpandToIFunctionAccessInFuncView(this);
        //		}, (p) => {
        //			return true;
        //		});
        //	}

        public int Depth { get { return 0; } set { } }
    }
}
