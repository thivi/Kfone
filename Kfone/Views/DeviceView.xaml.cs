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
    public sealed partial class DeviceView : Page
    {
        public DeviceView()
        {
            this.InitializeComponent();
            Frame.Navigate(typeof(DevicesPage));
            addDeviceButton.Visibility = Visibility.Visible;
            Frame.Navigated += (object sender, NavigationEventArgs e) => {
                if(e.SourcePageType == typeof(DevicesPage))
                {
                    addDeviceButton.Visibility = Visibility.Visible;
                }
            };
        }

        private void addDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_DevicePage));
            addDeviceButton.Visibility = Visibility.Collapsed;
        }

        private void cancelDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DevicesPage));
            addDeviceButton.Visibility = Visibility.Visible;
        }
    }
}
