using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kfone.Core.Helpers;
using Kfone.Core.Models;
using Windows.UI.Xaml.Controls;

namespace Kfone.Services
{
    public class AccessControl
    {
        private IdentityService _identityService => Singleton<IdentityService>.Instance;
        private Dictionary<string, Roles[]> _pageAccess = new Dictionary<string, Roles[]>();
        private Dictionary<string, Roles[]> _granularAccess = new Dictionary<string, Roles[]>();
      
        public AccessControl() {
            Roles[] all = { Roles.SalesRep, Roles.AdminG, Roles.Marketing };
            Roles[] adminOnly = { Roles.AdminG };
            Roles[] adminSales = { Roles.AdminG, Roles.SalesRep };
            Roles[] adminMarketing = { Roles.AdminG, Roles.Marketing };
            _pageAccess.Add("Profile", all);
            _pageAccess.Add("Devices", adminOnly);
            _pageAccess.Add("Services", adminOnly);
            _pageAccess.Add("Promotions", adminSales);
            _pageAccess.Add("Customers", adminMarketing);
            _pageAccess.Add("Staff", adminOnly);
            _pageAccess.Add("Dashboard", all);

            _granularAccess.Add("create_customers", adminOnly);
        }

        public bool ShouldShowPage(string page)
        {
            Roles currentUserRole = _identityService.GetUserRole();

            try
            {
                return _pageAccess[page].Contains(currentUserRole);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ShouldShowGranular(string item)
        {
            Roles currentUserRole = _identityService.GetUserRole();

            return _granularAccess[item].Contains(currentUserRole);
        }
    }
}
