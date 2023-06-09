﻿using System;

using Kfone.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class DevicesDetailControl : UserControl
    {
        public Device ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as Device; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(Device), typeof(DevicesDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public DevicesDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DevicesDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
