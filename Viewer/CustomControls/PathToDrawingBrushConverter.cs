using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PTR.Viewer.CustomControls {
	class PathToDrawingBrushConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (null == value) {
				return value;
			}
			Path p = (Path)value;
            string par = (string)parameter;
			var d = new DrawingBrush(new GeometryDrawing());
			var gd = new GeometryDrawing();
			d.Drawing = gd;
			System.Windows.Data.BindingOperations.SetBinding(d.Drawing, GeometryDrawing.GeometryProperty, new Binding() { Path = new PropertyPath("Data"), Source = p });
            //gd.Geometry = p.Data;
			gd.Brush = p.Fill;
            if (par != null)
                if (par.ToLower().Contains("nofill"))
                    gd.Brush = Brushes.Transparent;
            gd.Pen = new Pen(p.Stroke, p.StrokeThickness);
			d.Stretch = Stretch.Uniform;
			return d;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
