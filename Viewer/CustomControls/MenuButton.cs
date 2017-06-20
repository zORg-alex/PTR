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
    public partial class MenuButton : zButton {
        public MenuButton() {
            Click += (sender, e) => {
                if (IsTogglable) SetValue(IsToggledProperty, !IsToggled);
            };
        }

        [Category("Brush")]
        public Brush Icon {
            get { return (Brush)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Brush), typeof(MenuButton), new PropertyMetadata(new DrawingBrush()));
        
        [Category("Brush")]
        public Brush ActiveIcon {
            get { return (Brush)GetValue(ActiveIconProperty); }
            set { SetValue(ActiveIconProperty, value); }
        }
        public static readonly DependencyProperty ActiveIconProperty =
            DependencyProperty.Register("ActiveIcon", typeof(Brush), typeof(MenuButton), new PropertyMetadata(new DrawingBrush()));

        [Category("Appearance")]
        public double IconWidth {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(MenuButton), new PropertyMetadata(16d));

        [Category("Appearance")]
        public double IconHeight {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(MenuButton), new PropertyMetadata(16d));

        [Category("Appearance")]
        public bool IsCollapsed {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }
        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(MenuButton), new PropertyMetadata(false));

        [Category("Appearance")]
        public bool IsToggled {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }
        public static readonly DependencyProperty IsToggledProperty =
            DependencyProperty.Register("IsToggled", typeof(bool), typeof(MenuButton), new PropertyMetadata(false));

        [Category("Appearance")]
        public bool IsTogglable {
            get { return (bool)GetValue(IsTogglableProperty); }
            set { SetValue(IsTogglableProperty, value); }
        }
        public static readonly DependencyProperty IsTogglableProperty =
            DependencyProperty.Register("IsTogglable", typeof(bool), typeof(MenuButton), new PropertyMetadata(true));


    }
}
