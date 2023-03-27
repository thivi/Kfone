using System;

using Kfone.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Kfone.Views
{
    public sealed partial class LogInPage : Page
    {
        public LogInViewModel ViewModel { get; } = new LogInViewModel();

        public LogInPage()
        {
            InitializeComponent();
        }
    }
}
