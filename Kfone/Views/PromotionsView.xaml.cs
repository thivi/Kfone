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
    public sealed partial class PromotionsView : Page
    {
        public PromotionsView()
        {
            this.InitializeComponent();
            Frame.Navigate(typeof(PromotionsPage));
            addPromotionButton.Visibility = Visibility.Visible;
            cancelPromotionButton.Visibility = Visibility.Collapsed;
        }

        private void addPromotionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_PromotionPage));
            addPromotionButton.Visibility = Visibility.Collapsed;
            cancelPromotionButton.Visibility = Visibility.Visible;
        }

        private void cancelPromotionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PromotionsPage));
            addPromotionButton.Visibility = Visibility.Visible;
            cancelPromotionButton.Visibility = Visibility.Collapsed;
        }
    }
}
