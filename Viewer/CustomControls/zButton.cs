using EffectLibrary;
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
	public class zButton : Button, INotifyPropertyChanged {

		DesaturateEffect _desaturateEffect = new DesaturateEffect();
		OverlayEffect _overlayEffect = new OverlayEffect();

		public zButton() {
			//_desaturateEffect.DesaturationFactor = .25d;
			//_overlayEffect.Blend = Colors.Transparent;
			//Effect = _overlayEffect;

			//MouseEnter += MouseOver_;
			//MouseLeave += MouseOver_;
			//PreviewMouseDown += MouseDown_;
			//PreviewMouseUp += MouseDown_;
			//IsEnabledChanged += IsEnabledChanged_;
		}

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

		public static readonly DependencyProperty HighlightProperty =
			DependencyProperty.Register("Highlight", typeof(SolidColorBrush), typeof(zButton), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(22, 0xD1, 0xDD, 0xE5))));
		public static readonly DependencyProperty PressedProperty =
			DependencyProperty.Register("Pressed", typeof(SolidColorBrush), typeof(zButton), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(22, 0x01, 0x1F, 0x36))));
        public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyName) {
			// take a copy to prevent thread issues
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		//private void MouseDown_(object sender, MouseButtonEventArgs e) {
		//	if (e.ButtonState == MouseButtonState.Pressed) {
		//		if (Pressed != null) _overlayEffect.Blend = Pressed.Color;
		//	}
		//	else {
		//		MouseOver_(null, null);
		//	}
		//}

		//private void IsEnabledChanged_(object sender, DependencyPropertyChangedEventArgs e) {
		//	if (IsEnabled) {
		//		Effect = _overlayEffect;
		//	}
		//	else {
		//		Effect = _desaturateEffect;
		//	}
		//}

		//private void MouseOver_(object sender, MouseEventArgs e) {
		//	if (IsMouseOver) {
		//		if (Highlight != null) _overlayEffect.Blend = Highlight.Color;
		//	}
		//	else {
		//		_overlayEffect.Blend = Colors.Transparent;
		//	}
		//}
	}
}
