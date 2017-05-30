using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
	/// Interaction logic for EditableListBox.xaml
	/// </summary>
	public partial class EditableListBox : System.Windows.Controls.ListBox, INotifyPropertyChanged {
		public EditableListBox() {
			InitializeComponent();
			//var s = FindResource("EditableListBoxStyle") as Style;
			//Style = s;
		}

		[Category("Common")]
		public UVMCommand AddItem {
			get { return (UVMCommand)GetValue(AddItemProperty); }
			set {
				SetValue(AddItemProperty, value);
			}
		}

		// Using a DependencyProperty as the backing store for AddItem.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty AddItemProperty =
			DependencyProperty.Register("AddItem", typeof(UVMCommand), typeof(EditableListBox),
				new PropertyMetadata(
					new UVMCommand("Default Command", p => { Debug.WriteLine("EditableListBox.AddITem not defined"); })));

		[Category("Layout")]
		public Orientation Orientation {
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
		
		// Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof(Orientation), typeof(EditableListBox), new PropertyMetadata(Orientation.Vertical));
		
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName) {
			// take a copy to prevent thread issues
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
