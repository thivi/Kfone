using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kfone.Core.Helpers;
using Kfone.Core.Models;
using Kfone.Core.Services;
using Kfone.Helpers;
using Kfone.ViewModels;

using Windows.Storage;

namespace Kfone.Services
{
    public class UserDataService
    {
        private const string _userSettingsKey = "IdentityUser";

        private UserViewModel _user;

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        private MicrosoftGraphService MicrosoftGraphService => Singleton<MicrosoftGraphService>.Instance;

        public event EventHandler<UserViewModel> UserDataUpdated;

        public UserDataService()
        {
        }

        public void Initialize()
        {
            IdentityService.LoggedIn += OnLoggedIn;
            IdentityService.LoggedOut += OnLoggedOut;
        }

        public UserViewModel GetUser()
        {
            if (_user == null)
            {
                _user = GetDefaultUserData();
            }

            return _user;
        }

        private void OnLoggedIn(object sender, EventArgs e)
        {
            _user = GetDefaultUserData();
            UserDataUpdated?.Invoke(this, _user);
        }

        private async void OnLoggedOut(object sender, EventArgs e)
        {
            _user = null;
            await ApplicationData.Current.LocalFolder.SaveAsync<User>(_userSettingsKey, null);
        }

        private UserViewModel GetDefaultUserData()
        {
            return new UserViewModel()
            {
                Name = IdentityService.GetAccountUserName(),
                Photo = IdentityService.GetUserProfilePic(),
                Role = IdentityService.GetUserRole()
            };
        }
    }
}
