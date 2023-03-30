using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;

using Kfone.Core.Helpers;
using Kfone.Core.Models;
using Windows.UI.Xaml;

namespace Kfone.Services
{
    public class IdentityService
    {
        private OidcClient _client;
        private LoginResult _authenticationResult;
        public event EventHandler LoggedIn;

        public event EventHandler LoggedOut;
        private static IdentityMgtService managementService => Singleton<IdentityMgtService>.Instance;

        public void InitializeIdentityClient()
        {
            var options = new OidcClientOptions
            {
                Authority = $"https://api.asgardeo.io/t/{ConfigurationManager.AppSettings["Tenant"]}/oauth2/token",
                ClientId = ConfigurationManager.AppSettings["ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                Scope = "openid profile groups",
                RedirectUri = ConfigurationManager.AppSettings["RedirectURI"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["RedirectURI"],
                Browser = new WebView(),
                Policy = new Policy
                {
                    Discovery = new DiscoveryPolicy
                    {
                        AdditionalEndpointBaseAddresses =
                        {
                            $"https://api.asgardeo.io/t/{ConfigurationManager.AppSettings["Tenant"]}/oauth2",
                            $"https://api.asgardeo.io/t/{ConfigurationManager.AppSettings["Tenant"]}/oauth2/token",
                            $"https://api.asgardeo.io/t/{ConfigurationManager.AppSettings["Tenant"]}/oidc",
                            $"https://api.asgardeo.io/t/{ConfigurationManager.AppSettings["Tenant"]}"
                        }
                    }
                }
            };

            _client = new OidcClient(options);
        }

        public bool IsLoggedIn() => _authenticationResult != null;

        public async Task<LoginResultType> LoginAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return LoginResultType.NoNetworkAvailable;
            }

            try
            {
                _authenticationResult = await _client.LoginAsync(new LoginRequest());
                managementService.InitializeWithClientCredentials();

                if (!IsAuthorized())
                {
                    _authenticationResult = null;
                    return LoginResultType.Unauthorized;
                }

                LoggedIn?.Invoke(this, EventArgs.Empty);

                return LoginResultType.Success;
            }
            catch (Exception)
            {
                return LoginResultType.UnknownError;
            }
        }

        public bool IsAuthorized()
        {
            // TODO: You can also add extra authorization checks here.
            // i.e.: Checks permisions of _authenticationResult.Account.Username in a database.
            return this._authenticationResult != null;
        }

        public string GetAccountUserName()
        {

            return _authenticationResult?.User.Claims.Where(claim => claim.Type == "given_name").ToList()[0].Value;
        }

        public string GetUserProfilePic()
        {
            try
            {
                return _authenticationResult?.User.Claims?.Where(claim => claim.Type == "profile")?.ToList()?[0]?.Value;
            }
            catch (Exception)
            {
                return "https://cdn3.iconfinder.com/data/icons/web-design-and-development-2-6/512/87-1024.png";
            }
        }

        public Roles GetUserRole()
        {
            Roles role;
            const string adminG = "ApplicationAdmin";
            const string salesG = "SalesRep";
            const string marketingG = "MarketingLead";
            string groupName = _authenticationResult?.User?.Claims?.Where(claim => claim.Type == "groups").ToList()[0].Value;

            switch (groupName)
            {
                case adminG:
                    role = Roles.AdminG;

                    break;
                case salesG:
                    role = Roles.SalesRep;

                    break;
                case marketingG:
                    role = Roles.Marketing;

                    break;
                default:
                    role = Roles.Customer;
                    break;
            }

            return role;
        }

        public async Task LogoutAsync()
        {
            try
            {
                var logoutRequest = new LogoutRequest
                {
                    IdTokenHint = _authenticationResult.IdentityToken,
                    BrowserDisplayMode = IdentityModel.OidcClient.Browser.DisplayMode.Hidden
                };
                LogoutResult result = await _client.LogoutAsync(logoutRequest);
                NameValueCollection queryDict = System.Web.HttpUtility.ParseQueryString(new Uri(result.Response.ToString()).Query.ToString());
                if(queryDict.Get("error") == null)
                {
                    _authenticationResult = null;
                    LoggedOut?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                // TODO: LogoutAsync can fail please handle exceptions as appropriate to your scenario
                // For more info on MsalExceptions see
                // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/exceptions
            }
        }

        public string GetAccessToken()
        {
            return _authenticationResult.AccessToken;
        }
    }
}
