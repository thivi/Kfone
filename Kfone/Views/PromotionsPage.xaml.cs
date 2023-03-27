using System;

using Kfone.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class PromotionsPage : Page
    {
        public PromotionsViewModel ViewModel { get; } = new PromotionsViewModel();

        public PromotionsPage()
        {
            InitializeComponent();
            Loaded += PromotionsPage_Loaded;
        }

        private async void PromotionsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
