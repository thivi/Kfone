using System;
using Kfone.Core.Models;
using Kfone.Core.Services;
using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class Add_PromotionPage : Page
    {
        public Add_PromotionViewModel ViewModel { get; } = new Add_PromotionViewModel();

        public Add_PromotionPage()
        {
            InitializeComponent();
            DeviceService.GetDevices().ForEach(device =>
            {
                this.Device.Items.Add(device);
            });
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Promotion promotion = new Promotion { name=Name.Text, code=Code.Text, image= ImageURL.Text, item=(Device)Device.SelectedItem, percent=float.Parse(Percentage.Text) };
            ViewModel.AddPromotion(promotion);
            this.Frame.Navigate(typeof(PromotionsPage));
        }

        private void cancelButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DevicesPage));
        }
    }
}
