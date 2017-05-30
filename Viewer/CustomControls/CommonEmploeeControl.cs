using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public class CommonEmploeeControl : UserControl {

        public string AdditionalData {
            get { return (string)GetValue(AdditionalDataProperty); }
            set { SetValue(AdditionalDataProperty, value); }
        }
        public static readonly DependencyProperty AdditionalDataProperty =
            DependencyProperty.Register("AdditionalData", typeof(string), typeof(CommonEmploeeControl));

    }
}
