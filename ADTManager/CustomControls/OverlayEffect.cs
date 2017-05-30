using PTR.PTRLib.Common;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace ADTManager.CustomControls {
	public class OverlayEffect : ShaderEffect {

		private static PixelShader _pixelShader = new PixelShader();

		static OverlayEffect() {
			_pixelShader = new PixelShader();
			_pixelShader.UriSource = PackUriHelper.MakePackUri("CustomControls/Shader.ps", typeof(OverlayEffect).Assembly);
		}
		public OverlayEffect() {
			this.PixelShader = _pixelShader;
			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(BlendProperty);
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
		public Color Blend {
			get { return ((Color)(this.GetValue(BlendProperty))); }
			set { this.SetValue(BlendProperty, value); }
		}
		public static readonly DependencyProperty BlendProperty = DependencyProperty.Register("Blend", typeof(System.Windows.Media.Color), typeof(OverlayEffect), new PropertyMetadata(Colors.AliceBlue, PixelShaderConstantCallback(1)));
	}
}
