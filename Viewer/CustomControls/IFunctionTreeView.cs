using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public class IFunctionTreeView : TreeView {

        static IFunctionTreeView() {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(IFunctionViewTree), new FrameworkPropertyMetadata(typeof(IFunctionViewTree)));
        }

        public IFunctionTreeView() {
            var o = this;
            MouseEnter += (s, e) => {
                var a = o; //Debug
            };
        }
        
        public HierarchicalDataTemplate IFunctionAccessTemplate {
            get { return (HierarchicalDataTemplate)GetValue(IFunctionAccessTemplateProperty); }
            set { SetValue(IFunctionAccessTemplateProperty, value); }
        }
        public static readonly DependencyProperty IFunctionAccessTemplateProperty =
            DependencyProperty.Register("IFunctionAccessTemplate", typeof(HierarchicalDataTemplate), typeof(IFunctionTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public DataTemplate IQuestAccessTemplate {
            get { return (DataTemplate)GetValue(IQuestAccessTemplateProperty); }
            set { SetValue(IQuestAccessTemplateProperty, value); }
        }
        public static readonly DependencyProperty IQuestAccessTemplateProperty =
            DependencyProperty.Register("IQuestAccessTemplate", typeof(DataTemplate), typeof(IFunctionTreeView), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

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
