using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace EffectLibrary {
	public class DesaturateEffect : ShaderEffect {

		private static PixelShader _pixelShader = new PixelShader();

		static DesaturateEffect() {
			_pixelShader = new PixelShader();
			_pixelShader.UriSource = Global.MakePackUri("DesaturateEffect.ps");
        }
		public DesaturateEffect() {
			this.PixelShader = _pixelShader;
			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(DesaturationFactorProperty);
		}


		public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(DesaturateEffect), 0);
		public Brush Input {
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty DesaturationFactorProperty = DependencyProperty.Register("DesaturationFactor", typeof(double), typeof(DesaturateEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0), CoerceDesaturationFactor));
		public double DesaturationFactor {
			get { return (double)GetValue(DesaturationFactorProperty); }
			set { SetValue(DesaturationFactorProperty, value); }
		}

		private static object CoerceDesaturationFactor(DependencyObject d, object value) {
			DesaturateEffect effect = (DesaturateEffect)d;
			double newFactor = (double)value;

			if (newFactor < 0.0 || newFactor > 1.0) {
				return effect.DesaturationFactor;
			}

			return newFactor;
		}
	}
}
