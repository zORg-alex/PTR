using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace EffectLibrary {
    public class zBlend : ShaderEffect {
        #region Constructors

        static zBlend() {
            _pixelShader.UriSource = Global.MakePackUri("zBlend.ps");
        }

        public zBlend() {
            this.PixelShader = _pixelShader;
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(BlendProperty);
            //BindingOperations.SetBinding(BlendSolidBrush, OverlayEffect.BlendProperty, new Binding() { Path = new PropertyPath("Color") });
        }

        #endregion

        #region Dependency Properties

        public Brush Input {
            get {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set {
                this.SetValue(InputProperty, value);
            }
        }
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(zBlend), 0);

        public enum BlendModes { Normal=0, Darken=10, Multiply=11, Lighten=20, Screen=21, Overlay=30, Difference=40, Hue=50}

        [Category("Mode")]
        public BlendModes BlendMode {
            get { return (BlendModes)GetValue(BlendModeProperty); }
            set { SetValue(BlendModeProperty, value); }
        }
        public static readonly DependencyProperty BlendModeProperty =
            DependencyProperty.Register("BlendMode", typeof(BlendModes), typeof(zBlend), new PropertyMetadata(BlendModes.Normal, BlendModeChanged));

        private static void BlendModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            zBlend oe = d as zBlend;
            BlendModes nv = (BlendModes)(e.NewValue);
            if (oe != null) oe.SetValue(BlendModeIndexProperty, ((float)(int)nv));
        }

        public float BlendModeIndex {
            get { return (float)GetValue(BlendModeIndexProperty); }
            set { SetValue(BlendModeIndexProperty, value); }
        }
        public static readonly DependencyProperty BlendModeIndexProperty =
            DependencyProperty.Register("BlendModeIndex", typeof(float), typeof(zBlend), new PropertyMetadata(0f, PixelShaderConstantCallback(1)));


        [Category("Brush")]
        public SolidColorBrush BlendSolidBrush {
            get { return (SolidColorBrush)GetValue(BlendSolidBrushProperty); }
            set { SetValue(BlendSolidBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlendSolidBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlendSolidBrushProperty =
            DependencyProperty.Register("BlendSolidBrush", typeof(SolidColorBrush),
                typeof(zBlend), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), BlendColorSolidBrushChanged));

        private static void BlendColorSolidBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            zBlend oe = d as zBlend;
            if (oe != null && (e.NewValue as SolidColorBrush) != null) oe.SetValue(BlendProperty, (e.NewValue as SolidColorBrush).Color);
        }

        [Category("Brush")]
        public Color Blend {
            get { return ((Color)(this.GetValue(BlendProperty))); }
            set { this.SetValue(BlendProperty, value); }
        }
        public static readonly DependencyProperty BlendProperty = DependencyProperty.Register("Blend", typeof(System.Windows.Media.Color), 
            typeof(zBlend), new PropertyMetadata(Colors.AliceBlue, PixelShaderConstantCallback(0)));

        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
