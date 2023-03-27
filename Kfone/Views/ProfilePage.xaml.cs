using System;

using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class ProfilePage : Page
    {
        public ProfileViewModel ViewModel { get; } = new ProfileViewModel();

        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}
