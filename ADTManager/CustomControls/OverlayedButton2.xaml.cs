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

namespace ADTManager.CustomControls {
	/// <summary>
	/// Interaction logic for OverlayedButton.xaml
	/// </summary>
	public partial class OverlayedButton2 : Button, INotifyPropertyChanged {

		public OverlayedButton2() {
			UseMarginEffect = false;
			ShaderColor = Colors.Transparent;

			InitializeComponent();
			if (Highlight == null) Highlight = (SolidColorBrush)Resources.MergedDictionaries[0]["WhiteStyle.Overlay.Highlight.A"];
			if (Pressed == null) Pressed = (SolidColorBrush)Resources.MergedDictionaries[0]["WhiteStyle.Overlay.Pressed"];
			if (Disabled == null) Disabled = (SolidColorBrush)Resources.MergedDictionaries[0]["WhiteStyle.Overlay.Disabled"];
		}

		public Color ShaderColor {
			get { return (Color)GetValue(ShaderColorProperty); }
			set { SetValue(ShaderColorProperty, value); }
		}
		public static readonly DependencyProperty ShaderColorProperty =
			DependencyProperty.Register("ShaderColor", typeof(Color), typeof(OverlayedButton2));

		//public ColorBlend.Blends ShaderBlendMode {
		//	get { return (ColorBlend.Blends)GetValue(ShaderBlendModeProperty); }
		//	set { SetValue(ShaderBlendModeProperty, value); }
		//}
		//public static readonly DependencyProperty ShaderBlendModeProperty =
		//	DependencyProperty.Register("ShaderBlendMode", typeof(ColorBlend.Blends), typeof(OverlayedButton2), new FrameworkPropertyMetadata(ColorBlend.Blends.Screen) { AffectsRender = true});


		[Category("Brush")]
		public SolidColorBrush Highlight {
			get { return GetValue(HighlightProperty) as SolidColorBrush; }
			set {
				SetValue(HighlightProperty, value);
			}
		}
		[Category("Brush")]
		public SolidColorBrush Pressed {
			get { return GetValue(PressedProperty) as SolidColorBrush; }
			set {
				SetValue(PressedProperty, value);
			}
		}
		[Category("Brush")]
		public SolidColorBrush Disabled {
			get { return GetValue(DisabledProperty) as SolidColorBrush; }
			set {
				SetValue(DisabledProperty, value);
			}
		}

		public bool? UseMarginEffect {
			get { return (bool?)GetValue(UseMarginEffectProperty); }
			set { SetValue(UseMarginEffectProperty, value); }
		}

		public static readonly DependencyProperty HighlightProperty =
			DependencyProperty.Register("Highlight", typeof(SolidColorBrush), typeof(OverlayedButton2));
		public static readonly DependencyProperty PressedProperty =
			DependencyProperty.Register("Pressed", typeof(SolidColorBrush), typeof(OverlayedButton2));
		public static readonly DependencyProperty DisabledProperty =
			DependencyProperty.Register("Disabled", typeof(SolidColorBrush), typeof(OverlayedButton2));
		public static readonly DependencyProperty UseMarginEffectProperty =
			DependencyProperty.Register("UseMarginEffect", typeof(bool?), typeof(OverlayedButton2));



		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyName) {
			// take a copy to prevent thread issues
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void MouseDown_(object sender, MouseButtonEventArgs e) {
			//Console.WriteLine("MouseDown");
			if (e.ButtonState == MouseButtonState.Pressed) {
				ShaderColor = Pressed.Color;
			} else {
				if (IsMouseOver) {
					ShaderColor = Highlight.Color;
				} else {
					ShaderColor = Colors.Transparent;
				}
			}
		}

		private void this_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (IsMouseOver) {
				ShaderColor = Disabled.Color;
			} else {
				if (IsMouseOver) {
					ShaderColor = Highlight.Color;
				} else {
					ShaderColor = Colors.Transparent;
				}
			}
		}

		private void MouseOver(object sender, MouseEventArgs e) {
			//Console.WriteLine("MouseOver");
			if (IsMouseOver) {
				ShaderColor = Highlight.Color;
			} else {
				ShaderColor = Colors.Transparent;
			}
		}
	}
}

