﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public class FilterBox : TextBox {

        public string Placeholder {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(FilterBox), new PropertyMetadata("Type here..."));
    }
}
