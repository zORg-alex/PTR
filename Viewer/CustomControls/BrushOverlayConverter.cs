﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PTR.Viewer.CustomControls {
    public class BrushOverlayConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return value;
            }
            string par = (string)parameter;
            Color color = (value.GetType() == typeof(Color)) ? (Color)value : (value.GetType() == typeof(SolidColorBrush)) ? ((SolidColorBrush)value).Color : Colors.Transparent;
            Color Blend = Colors.Transparent;
            if (par.Length == 9 && par[0] == '#') {
                Blend = Color.FromArgb(
                    byte.Parse(par.Substring(1, 2), NumberStyles.HexNumber),
                    byte.Parse(par.Substring(3, 2), NumberStyles.HexNumber),
                    byte.Parse(par.Substring(5, 2), NumberStyles.HexNumber),
                    byte.Parse(par.Substring(7, 2), NumberStyles.HexNumber)
                );
            }
            var c = Color.Add(color, Blend);
            return new SolidColorBrush(c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
