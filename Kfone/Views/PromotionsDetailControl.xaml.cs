using System;

using Kfone.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class PromotionsDetailControl : UserControl
    {
        public Promotion ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as Promotion; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(Promotion), typeof(PromotionsDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public PromotionsDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PromotionsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
