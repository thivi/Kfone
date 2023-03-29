using System;

using Kfone.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class CustomersDetailControl : UserControl
    {
        public Customer ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as Customer; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(Customer), typeof(CustomersDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public CustomersDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomersDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
