using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PTR.Viewer.CustomControls {
    public class PVStructuralTreeView : TreeView {

        [Category("Common")]
        public object SelectedObject {
            get { return (object)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register("SelectedObject", typeof(object), typeof(PVStructuralTreeView), new PropertyMetadata(null));

        [Category("Common")]
        public bool IsAltObjectAvailable {
            get { return (bool)GetValue(IsAltObjectAvailableProperty); }
            set { SetValue(IsAltObjectAvailableProperty, value); }
        }
        public static readonly DependencyProperty IsAltObjectAvailableProperty =
            DependencyProperty.Register("IsAltObjectAvailable", typeof(bool), typeof(PVStructuralTreeView), new PropertyMetadata(false));

        [Category("Common")]
        public object SelectedAltObject {
            get { return (object)GetValue(SelectedAltObjectProperty); }
            set { SetValue(SelectedAltObjectProperty, value); }
        }
        public static readonly DependencyProperty SelectedAltObjectProperty =
            DependencyProperty.Register("SelectedAltObject", typeof(object), typeof(PVStructuralTreeView), new PropertyMetadata(null));

        private object _oldObject;
        public object OldObject {
            get { return _oldObject; }
            set { _oldObject = value; }
        }
        
        public DataTemplate PVEmploeeTemplate {
            get { return (DataTemplate)GetValue(PVEmploeeTemplateProperty); }
            set { SetValue(PVEmploeeTemplateProperty, value); }
        }
        public static readonly DependencyProperty PVEmploeeTemplateProperty =
            DependencyProperty.Register("PVEmploeeTemplate", typeof(DataTemplate), typeof(PVStructuralTreeView), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate PVStructuralTemplate {
            get { return (HierarchicalDataTemplate)GetValue(PVStructuralTemplateProperty); }
            set { SetValue(PVStructuralTemplateProperty, value); }
        }
        public static readonly DependencyProperty PVStructuralTemplateProperty =
            DependencyProperty.Register("PVStructuralTemplate", typeof(HierarchicalDataTemplate), typeof(PVStructuralTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public DataTemplate UUserTemplate {
            get { return (DataTemplate)GetValue(UUserTemplateProperty); }
            set { SetValue(UUserTemplateProperty, value); }
        }
        public static readonly DependencyProperty UUserTemplateProperty =
            DependencyProperty.Register("UUserTemplate", typeof(DataTemplate), typeof(PVStructuralTreeView), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TreeView c = d as TreeView;
            if (c != null) {
                var key = new DataTemplateKey((e.NewValue as DataTemplate).DataType);
                c.Resources.Remove(key);
                c.Resources.Add(key, e.NewValue);
            }
        }
        
        public PVStructuralTreeView() {
            MouseDoubleClick += (s, e) => {
                if (IsAltObjectAvailable) {
                    SelectedAltObject = SelectedObject;
                    SelectedObject = OldObject;
                }
            };
            SelectedItemChanged += (s,e) => {
                OldObject = SelectedObject;
                SelectedObject = SelectedItem;
            }; //Because SelectedItem is not accessible
        }
    }
}
