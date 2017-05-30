using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public class ADFolderTreeView : TreeView {

        public ADFolderTreeView() {
            SelectedItemChanged += (s, e) => {
                SelectedObject = SelectedItem;
            }; //Because SelectedItem is not accessible
        }

        [Category("Common")]
        public object SelectedObject {
            get { return (object)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register("SelectedObject", typeof(object), typeof(ADFolderTreeView), new PropertyMetadata(null));

        public HierarchicalDataTemplate ADFolderTemplate {
            get { return (HierarchicalDataTemplate)GetValue(ADFolderTemplateProperty); }
            set { SetValue(ADFolderTemplateProperty, value); }
        }
        public static readonly DependencyProperty ADFolderTemplateProperty =
            DependencyProperty.Register("ADFolderTemplate", typeof(HierarchicalDataTemplate), typeof(ADFolderTreeView), new PropertyMetadata( new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TreeView c = d as TreeView;
            if (c != null) {
                var key = new DataTemplateKey((e.NewValue as DataTemplate).DataType);
                c.Resources.Remove(key);
                c.Resources.Add(key, e.NewValue);
            }
        }
    }
}
