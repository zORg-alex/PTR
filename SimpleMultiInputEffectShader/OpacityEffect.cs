using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace EffectLibrary {
    public class OpacityEffect : ShaderEffect {
        #region Constructors

        static OpacityEffect() {
            _pixelShader.UriSource = Global.MakePackUri("OpacityEffect.ps");
        }

        public OpacityEffect() {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OpacityAmountProperty);
        }

        #endregion

        #region Dependency Properties

        public Brush Input {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        // Brush-valued properties turn into sampler-property in the shader.
        // This helper sets "ImplicitInput" as the default, meaning the default
        // sampler is whatever the rendering of the element it's being applied to is.
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(OpacityEffect), 0);



        public float OpacityAmount {
            get { return (float)GetValue(OpacityAmountProperty); }
            set { SetValue(OpacityAmountProperty, value); }
        }

        // Scalar-valued properties turn into shader constants with the register
        // number sent into PixelShaderConstantCallback().
        public static readonly DependencyProperty OpacityAmountProperty =
            DependencyProperty.Register("OpacityAmount", typeof(float), typeof(OpacityEffect),
                    new UIPropertyMetadata(1f, PixelShaderConstantCallback(0)));

        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
