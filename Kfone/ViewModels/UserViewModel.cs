﻿using System;
using Kfone.Core.Models;
using Kfone.Helpers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

using Windows.UI.Xaml.Media.Imaging;

namespace Kfone.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private string _name;
        private string _userPrincipalName;
        private string _photo;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string UserPrincipalName
        {
            get => _userPrincipalName;
            set => SetProperty(ref _userPrincipalName, value);
        }

        public string Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        public Roles Role { get; set; }

        public UserViewModel()
        {
        }
    }
}
