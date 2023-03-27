using System;

using Kfone.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class DevicesPage : Page
    {
        public DevicesViewModel ViewModel { get; } = new DevicesViewModel();

        public DevicesPage()
        {
            InitializeComponent();
            Loaded += DevicesPage_Loaded;
        }

        private async void DevicesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
