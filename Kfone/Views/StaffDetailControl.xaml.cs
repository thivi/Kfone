using System;

using Kfone.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class StaffDetailControl : UserControl
    {
        public SampleOrder ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as SampleOrder; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(SampleOrder), typeof(StaffDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public StaffDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as StaffDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
