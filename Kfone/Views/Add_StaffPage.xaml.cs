using System;

using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class Add_StaffPage : Page
    {
        public Add_StaffViewModel ViewModel { get; } = new Add_StaffViewModel();

        public Add_StaffPage()
        {
            InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
