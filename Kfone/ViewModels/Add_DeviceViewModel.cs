using System;
using Kfone.Core.Models;
using Kfone.Core.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Kfone.ViewModels
{
    public class Add_DeviceViewModel : ObservableObject
    {
        public Add_DeviceViewModel()
        {
        }

        public void AddDevice(Device device)
        {
            DeviceService.SetDevice(device);
        }
    }
}
