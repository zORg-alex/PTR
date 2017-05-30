using PTR.Viewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PTR.Viewer.CustomControls {
	/// <summary>
	/// Interaction logic for WindowButtons.xaml
	/// </summary>
	public partial class WindowButtons : UserControl {
		
		//[Category("Appearance")]
		//public WindowState State {
		//	get {
		//		return (WindowState)GetValue(StateProperty);
		//	}
		//	set {
		//		SetValue(StateProperty, value);
		//		if (value == WindowState.Normal) {
		//			RestoreIconVisibility = Visibility.Collapsed;
		//			MaximizeIconVisibility = Visibility.Visible;
		//			MaximizeRestore.ToolTip = FirstFloor.ModernUI.Resources.Maximize;
		//			MaximizeRestore.Command = SystemCommands.MaximizeWindowCommand;
		//		}
		//		if (value == WindowState.Maximized) {
		//			RestoreIconVisibility = Visibility.Visible;
		//			MaximizeIconVisibility = Visibility.Collapsed;
		//			MaximizeRestore.ToolTip = FirstFloor.ModernUI.Resources.Restore;
		//			MaximizeRestore.Command = SystemCommands.RestoreWindowCommand;
		//		}
		//	}
		//}
		
		//// Using a DependencyProperty as the backing store for WindowState.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty StateProperty =
		//	DependencyProperty.Register("State", typeof(WindowState), typeof(WindowButtons));
		
		//public Visibility RestoreIconVisibility {
		//	get { return (Visibility)GetValue(RestoreIconVisibilityProperty); }
		//	set { SetValue(RestoreIconVisibilityProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for RestoreIconVisibility.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty RestoreIconVisibilityProperty =
		//	DependencyProperty.Register("RestoreIconVisibility", typeof(Visibility), typeof(WindowButtons), new UIPropertyMetadata(Visibility.Collapsed));
		
		//public Visibility MaximizeIconVisibility {
		//	get { return (Visibility)GetValue(MaximizeIconVisibilityProperty); }
		//	set { SetValue(MaximizeIconVisibilityProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for MaximizeIconVisibility.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty MaximizeIconVisibilityProperty =
		//	DependencyProperty.Register("MaximizeIconVisibility", typeof(Visibility), typeof(WindowButtons), new UIPropertyMetadata(Visibility.Visible));
		
		public WindowButtons() {
			InitializeComponent();
			//State = WindowState.Maximized;
			//var b = new Binding();
			//b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(FirstFloor.ModernUI.Windows.Controls.ModernWindow), 1);
			//b.Mode = BindingMode.OneWay;
			//b.Path = new PropertyPath("WindowState", null);
			//var a = 0;
		}

		private void Close_Click(object sender, RoutedEventArgs e) {
			App.Current.Shutdown();
		}
	}
}
