using System.Windows;
using System.Windows.Controls;

namespace View.Helpers
{
    public class ExtendedTreeView : TreeView
    {
        public ExtendedTreeView() : base()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
        }

        private void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }

        public object WritableSelectedItem
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }

        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("WritableSelectedItem", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));
    }
}
