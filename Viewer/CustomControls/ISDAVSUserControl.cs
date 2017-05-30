using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public partial class ISDAVSUserControl : UserControl {

        [Category("Common")]
        public bool? IsExtended {
            get { return (bool?)GetValue(IsExtendedProperty); }
            set { SetValue(IsExtendedProperty, value); }
        }
        public static readonly DependencyProperty IsExtendedProperty =
            DependencyProperty.Register("IsExtended", typeof(bool?), typeof(ISDAVSUserControl),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

    }
}
