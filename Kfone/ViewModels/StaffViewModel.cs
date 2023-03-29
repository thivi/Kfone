using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Kfone.Core.Models;
using Kfone.Core.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Kfone.ViewModels
{
    public class StaffViewModel : ObservableObject
    {
        private Staff _selected;

        public Staff Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Staff> SampleItems { get; private set; } = new ObservableCollection<Staff>();

        public StaffViewModel()
        {
        }

        public async Task LoadDataAsync(ListDetailsViewState viewState)
        {
            SampleItems.Clear();

            var data = StaffService.GetStaffs();

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
