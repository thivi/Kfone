﻿using System;

using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardViewModel ViewModel { get; } = new DashboardViewModel();

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}
