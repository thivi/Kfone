using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Kfone.Core.Models;
using Kfone.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Kfone.ViewModels
{
    public class CustomersViewModel : ObservableObject
    {
        private Customer _selected;

        public Customer Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Customer> SampleItems { get; private set; } = new ObservableCollection<Customer>();

        public CustomersViewModel()
        {
        }

        public async Task LoadDataAsync(ListDetailsViewState viewState)
        {
            SampleItems.Clear();

            var data = await CustomerService.GetUsers();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            if (viewState == ListDetailsViewState.Both)
            {
                Selected = SampleItems.First();
            }
        }
    }
}
