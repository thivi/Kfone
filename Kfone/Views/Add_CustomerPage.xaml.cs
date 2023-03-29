using System;
using Kfone.Core.Models;
using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class Add_CustomerPage : Page
    {
        public Add_CustomerViewModel ViewModel { get; } = new Add_CustomerViewModel();

        public Add_CustomerPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Tiers tier;
            if(GoldTier.IsChecked == true) {
                tier = Tiers.Gold;
            } else if(SilverTier.IsChecked == true)
            {
                tier = Tiers.Silver;
            } else if(PlatinumTier.IsChecked==true)
            {
                tier = Tiers.Platinum;
            } else
            {
                tier = Tiers.Default;
            }
            Customer customer = new Customer { email=Email.Text, name = Name.Text, address = "", contactNo = "", dob = (DateTimeOffset)dob.Date, profilePic = profilePic.Text, tier = tier };
            ViewModel.AddCustomer(customer);
            this.Frame.Navigate(typeof(CustomersPage));
        }
    }
}
