using System;
using Kfone.Core.Models;
using Kfone.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Kfone.ViewModels
{
    public class Add_CustomerViewModel : ObservableObject
    {
        public Add_CustomerViewModel()
        {
        }

        public async void AddCustomer(Customer customer)
        {
            await CustomerService.AddCustomer(customer);
        }
    }
}
