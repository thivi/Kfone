using System;
using Kfone.Core.Models;
using Kfone.ViewModels;
using Remotion.Linq.Clauses.ResultOperators;
using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class Add_DevicePage : Page
    {
        public Add_DeviceViewModel ViewModel { get; } = new Add_DeviceViewModel();

        public Add_DevicePage()
        {
            InitializeComponent();
            TypeComboBox.Items.Add(DeviceType.Smartphone);
            TypeComboBox.Items.Add(DeviceType.Wearable);
            TypeComboBox.Items.Add(DeviceType.Tablet);
            TypeComboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string name = Name.Text;
            float price = float.Parse(Price.Text);
            string brand = Brand.Text;
            string image = ImageURL.Text;
            DeviceType deviceType = (DeviceType)TypeComboBox.SelectedItem;

            Device device = new Device { type = deviceType, name = name, price = price, brand = brand, image = image };

            ViewModel.AddDevice(device);

            this.Frame.Navigate(typeof(DevicesPage));
        }
    }
}
