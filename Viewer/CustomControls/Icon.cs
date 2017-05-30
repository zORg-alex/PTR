using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PTR.Viewer.CustomControls {
    public class Icon : Control {
        
        [Category("Appearance")]
        public Path IconShape {
            get { return (Path)GetValue(IconShapeProperty); }
            set { SetValue(IconShapeProperty, value); }
        }
        public static readonly DependencyProperty IconShapeProperty =
            DependencyProperty.Register("IconShape", typeof(Path), typeof(Icon), new PropertyMetadata(null));

        [Category("Appearance")]
        public Path AltIconShape {
            get { return (Path)GetValue(AltIconShapeProperty); }
            set { SetValue(AltIconShapeProperty, value); }
        }
        public static readonly DependencyProperty AltIconShapeProperty =
            DependencyProperty.Register("AltIconShape", typeof(Path), typeof(Icon), new PropertyMetadata(null));

        [Category("Common")]
        public bool SetAltIcon {
            get { return (bool)GetValue(SetAltIconProperty); }
            set { SetValue(SetAltIconProperty, value); }
        }
        public static readonly DependencyProperty SetAltIconProperty =
            DependencyProperty.Register("SetAltIcon", typeof(bool), typeof(Icon), new PropertyMetadata(false));

    }
}
