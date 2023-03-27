using System;

using Kfone.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class CustomersPage : Page
    {
        public CustomersViewModel ViewModel { get; } = new CustomersViewModel();

        public CustomersPage()
        {
            InitializeComponent();
            Loaded += CustomersPage_Loaded;
        }

        private async void CustomersPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
