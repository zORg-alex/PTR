using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PTR.Viewer.CustomControls {
    public class UserTreeView : TreeView {

        public UserTreeView() {
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
            DependencyProperty.Register("SelectedObject", typeof(object), typeof(UserTreeView), new PropertyMetadata(null));

        public HierarchicalDataTemplate PVEmploeeTemplate {
            get { return (HierarchicalDataTemplate)GetValue(PVEmploeeTemplateProperty); }
            set { SetValue(PVEmploeeTemplateProperty, value); }
        }
        public static readonly DependencyProperty PVEmploeeTemplateProperty =
            DependencyProperty.Register("PVEmploeeTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public DataTemplate PVHistEmploee {
            get { return (DataTemplate)GetValue(PVHistEmploeeProperty); }
            set { SetValue(PVHistEmploeeProperty, value); }
        }
        public static readonly DependencyProperty PVHistEmploeeProperty =
            DependencyProperty.Register("PVHistEmploee", typeof(DataTemplate), typeof(UserTreeView), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate ADUserTemplate {
            get { return (HierarchicalDataTemplate)GetValue(ADUserTemplateProperty); }
            set { SetValue(ADUserTemplateProperty, value); }
        }
        public static readonly DependencyProperty ADUserTemplateProperty =
            DependencyProperty.Register("ADUserTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate DrivePermissionsTemplate {
            get { return (HierarchicalDataTemplate)GetValue(DrivePermissionsTemplateProperty); }
            set { SetValue(DrivePermissionsTemplateProperty, value); }
        }
        public static readonly DependencyProperty DrivePermissionsTemplateProperty =
            DependencyProperty.Register("DrivePermissionsTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate ADFolderTemplate {
            get { return (HierarchicalDataTemplate)GetValue(ADFolderTemplateProperty); }
            set { SetValue(ADFolderTemplateProperty, value); }
        }
        public static readonly DependencyProperty ADFolderTemplateProperty =
            DependencyProperty.Register("ADFolderTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate ADFolderAccessTemplate {
            get { return (HierarchicalDataTemplate)GetValue(ADFolderAccessTemplateProperty); }
            set { SetValue(ADFolderAccessTemplateProperty, value); }
        }
        public static readonly DependencyProperty ADFolderAccessTemplateProperty =
            DependencyProperty.Register("ADFolderAccessTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public DataTemplate ISDAVSUserTemplate {
            get { return (DataTemplate)GetValue(ISDAVSUserTemplateProperty); }
            set { SetValue(ISDAVSUserTemplateProperty, value); }
        }
        public static readonly DependencyProperty ISDAVSUserTemplateProperty =
            DependencyProperty.Register("ISDAVSUserTemplate", typeof(DataTemplate), typeof(UserTreeView), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate IFunctionAccessTemplate {
            get { return (HierarchicalDataTemplate)GetValue(IFunctionAccessTemplateProperty); }
            set { SetValue(IFunctionAccessTemplateProperty, value); }
        }
        public static readonly DependencyProperty IFunctionAccessTemplateProperty =
            DependencyProperty.Register("IFunctionAccessTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

        public HierarchicalDataTemplate IQuestAccessTemplate {
            get { return (HierarchicalDataTemplate)GetValue(IQuestAccessTemplateProperty); }
            set { SetValue(IQuestAccessTemplateProperty, value); }
        }
        public static readonly DependencyProperty IQuestAccessTemplateProperty =
            DependencyProperty.Register("IQuestAccessTemplate", typeof(HierarchicalDataTemplate), typeof(UserTreeView), new PropertyMetadata(new HierarchicalDataTemplate(), new PropertyChangedCallback(OnTemplateChanged)));

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
