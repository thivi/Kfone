using System;
using System.Collections.Generic;
using Kfone.Services;
using Kfone.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    // TODO: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();
        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
            AccessControl accessControl = new AccessControl();
            using (IEnumerator<object> menuItems = (IEnumerator<object>)navigationView.MenuItems.GetEnumerator())
            {
                while (menuItems.MoveNext())
                {
                    Microsoft.UI.Xaml.Controls.NavigationViewItem menuItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)menuItems.Current;
                    bool shouldShow = accessControl.ShouldShowPage(menuItem.Content.ToString());
                    menuItem.Visibility = shouldShow? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
