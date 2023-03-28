using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;

using Kfone.Core.Helpers;
using Windows.Media.Protection.PlayReady;

namespace Kfone.Services
{
    public class IdentityService
    {
        // For more information about using Identity, see
        // https://github.com/microsoft/TemplateStudio/blob/main/docs/UWP/services/identity.md
        //
        // Read more about Microsoft Identity Client here
        // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki
        // https://docs.microsoft.com/azure/active-directory/develop/v2-overview

        // TODO: Please create a ClientID following these steps and update the app.config IdentityClientId.
        // https://docs.microsoft.com/azure/active-directory/develop/quickstart-register-app
        private readonly string _clientId = ConfigurationManager.AppSettings["IdentityClientId"];

        private readonly string _redirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

        private readonly string[] _graphScopes = new string[] { "user.read" };

        private bool _integratedAuthAvailable;
        private OidcClient _client;
        private LoginResult _authenticationResult;
        public event EventHandler LoggedIn;

        public event EventHandler LoggedOut;

        public void InitializeWithAadAndPersonalMsAccounts()
        {
            _integratedAuthAvailable = false;

            var options = new OidcClientOptions
            {
                Authority = "https://api.asgardeo.io/t/kfoneteam2/oauth2/token",
                ClientId = "lp79hGwq602Hx7jjhDuTlv08EOMa",
                ClientSecret = "MfyTzRqPhHROq4MIQ2l67rs_woMa",
                Scope = "openid profile",
                RedirectUri = "kfone://callback",
                Browser = new SystemBrowser(),
                Policy = new Policy
                {
                    Discovery = new IdentityModel.Client.DiscoveryPolicy
                    {
                        AdditionalEndpointBaseAddresses =
                        {
                            "https://api.asgardeo.io/t/kfoneteam2/oauth2",
                            "https://api.asgardeo.io/t/kfoneteam2/oauth2/token",
                            "https://api.asgardeo.io/t/kfoneteam2/oidc",
                            "https://api.asgardeo.io/t/kfoneteam2"
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
                var httpClient = new HttpClient();
                var disco = await httpClient.GetDiscoveryDocumentAsync("https://api.asgardeo.io/t/kfoneteam2/oauth2/token/.well-known/openid-configuration");
                if (disco.IsError) throw new Exception(disco.Error);
                _authenticationResult = await _client.LoginAsync(new LoginRequest());

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
            return _authenticationResult.User.Claims.Where(claim => claim.Type == "given_name").ToList()[0].Value;
        }

        public async Task LogoutAsync()
        {
            try
            {
                _authenticationResult = null;
                LoggedOut?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                // TODO: LogoutAsync can fail please handle exceptions as appropriate to your scenario
                // For more info on MsalExceptions see
                // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/exceptions
            }
        }

        public async Task<string> GetAccessTokenForGraphAsync() => await GetAccessTokenAsync(_graphScopes);

        public async Task<string> GetAccessTokenAsync(string[] scopes)
        {
            var acquireTokenSuccess = await AcquireTokenSilentAsync(scopes);
            if (acquireTokenSuccess)
            {
                return _authenticationResult.AccessToken;
            }
            else
            { 
                    // AcquireTokenSilent and AcquireTokenInteractive failed, the session will be closed.
                    _authenticationResult = null;
                    LoggedOut?.Invoke(this, EventArgs.Empty);
                    return string.Empty;
            }
        }

        public async Task<bool> AcquireTokenSilentAsync() => await AcquireTokenSilentAsync(_graphScopes);

        private async Task<bool> AcquireTokenSilentAsync(string[] scopes)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }

            try
            {
                _authenticationResult = await _client.LoginAsync(new LoginRequest());

                if (this.IsAuthorized())
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                // TODO: Silentauth failed, please handle this exception as appropriate to your scenario
                // For more info on MsalExceptions see
                // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/exceptions
                return false;
            }
        }
    }
}
