using Kfone.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kfone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerView : Page
    {
        public CustomerView()
        {
            this.InitializeComponent();
            Frame.Navigate(typeof(CustomersPage));
            if (new AccessControlService().ShouldShowGranular("create_customers"))
            {
                addCustomerButton.Visibility = Visibility.Visible;
            } else
            {
                addCustomerButton.Visibility = Visibility.Collapsed;
            }
            Frame.Navigated += (object sender, NavigationEventArgs e) => {
                if (e.SourcePageType == typeof(DevicesPage))
                {
                    addCustomerButton.Visibility = Visibility.Visible;
                }
            };
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_CustomerPage));
            addCustomerButton.Visibility = Visibility.Collapsed;
        }

        private void cancelCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CustomersPage));
            addCustomerButton.Visibility = Visibility.Visible;
        }
    }
}
