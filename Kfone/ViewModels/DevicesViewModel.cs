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
    public class DevicesViewModel : ObservableObject
    {
        private Device _selected;

        public Device Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Device> SampleItems { get; private set; } = new ObservableCollection<Device>();

        public DevicesViewModel()
        {
        }

        public async Task LoadDataAsync(ListDetailsViewState viewState)
        {
            SampleItems.Clear();

            var data = DeviceService.GetDevices();
            if (data.Count != 0)
            {
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
}
