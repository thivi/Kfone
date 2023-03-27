using System;

using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kfone.Views
{
    public sealed partial class Sales_trendPage : Page
    {
        public Sales_trendViewModel ViewModel { get; } = new Sales_trendViewModel();

        // TODO: Change the chart as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/radchart/getting-started
        public Sales_trendPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }
    }
}
