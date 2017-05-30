using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace EffectLibrary {
	public class OverlayEffect : ShaderEffect {

		private static PixelShader _pixelShader = new PixelShader();

		static OverlayEffect() {
			_pixelShader = new PixelShader();
			_pixelShader.UriSource = Global.MakePackUri("OverlayEffect.ps");
        }
		public OverlayEffect() {
			this.PixelShader = _pixelShader;
			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(BlendProperty);
			//BindingOperations.SetBinding(BlendSolidBrush, OverlayEffect.BlendProperty, new Binding() { Path = new PropertyPath("Color") });
		}

		public Brush Input {
			get {
				return ((Brush)(this.GetValue(InputProperty)));
			}
			set {
				this.SetValue(InputProperty, value);
			}
		}
		public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(OverlayEffect), 0);

		[Category("Brush")]
		public SolidColorBrush BlendSolidBrush {
			get { return (SolidColorBrush)GetValue(BlendSolidBrushProperty); }
			set { SetValue(BlendSolidBrushProperty, value); }
		}

		// Using a DependencyProperty as the backing store for BlendSolidBrush.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BlendSolidBrushProperty =
			DependencyProperty.Register("BlendSolidBrush", typeof(SolidColorBrush),
				typeof(OverlayEffect), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), BlendColorSolidBrushChanged));

		private static void BlendColorSolidBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			OverlayEffect oe = d as OverlayEffect;
			if (oe != null && (e.NewValue as SolidColorBrush) != null) oe.SetValue(BlendProperty, (e.NewValue as SolidColorBrush).Color);
		}

		[Category("Brush")]
		public Color Blend {
			get { return ((Color)(this.GetValue(BlendProperty))); }
			set { this.SetValue(BlendProperty, value); }
		}
		public static readonly DependencyProperty BlendProperty = DependencyProperty.Register("Blend", typeof(System.Windows.Media.Color), typeof(OverlayEffect), new PropertyMetadata(Colors.AliceBlue, PixelShaderConstantCallback(1)));
	}
}
