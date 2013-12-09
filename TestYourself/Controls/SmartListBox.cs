using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TestYourself.Controls
{
    public class SmartListBox : ListBox
    {
        public SmartListBox()
            : base()
        {
            SelectionChanged += SmartListBoxSelectionChanged;
            BindableSelectedItemsProperty = DependencyProperty.Register("BindableSelectedItems", typeof(IList), typeof(SmartListBox), null);
        }

        void SmartListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = new List<object>();
            items.AddRange(SelectedItems.Cast<object>());
            BindableSelectedItems = items;
        }

        public static DependencyProperty BindableSelectedItemsProperty;
        public IList BindableSelectedItems
        {
            get { return (IList)GetValue(BindableSelectedItemsProperty); }
            set { SetValue(BindableSelectedItemsProperty, value); }
        }
    }
}
