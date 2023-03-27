using System;

using Kfone.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class StaffPage : Page
    {
        public StaffViewModel ViewModel { get; } = new StaffViewModel();

        public StaffPage()
        {
            InitializeComponent();
            Loaded += StaffPage_Loaded;
        }

        private async void StaffPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
