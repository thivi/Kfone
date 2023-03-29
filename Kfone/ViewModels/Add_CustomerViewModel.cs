using System;
using Kfone.Core.Models;
using Kfone.Core.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Kfone.ViewModels
{
    public class Add_CustomerViewModel : ObservableObject
    {
        public Add_CustomerViewModel()
        {
        }

        public void AddCustomer(Customer customer)
        {
            CustomerService.SetCustomer(customer);
        }
    }
}
