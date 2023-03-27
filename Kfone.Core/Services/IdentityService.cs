using System;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using Kfone.Core.Helpers;

namespace Kfone.Core.Services
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
        private object _client;
        private object _authenticationResult;

        public event EventHandler LoggedIn;

        public event EventHandler LoggedOut;

        public void InitializeWithAadAndPersonalMsAccounts()
        {
            _integratedAuthAvailable = false;
            _client = null;
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
                _authenticationResult = null;
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
            return true;
        }

        public string GetAccountUserName()
        {
            return "Asgardeo User";
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
                return "_authenticationResult.AccessToken";
            }
            else
            {
                try
                {
                    // Interactive authentication is required
                    _authenticationResult = null;
                    return "_authenticationResult.AccessToken";
                }
                catch (Exception)
                {
                    // AcquireTokenSilent and AcquireTokenInteractive failed, the session will be closed.
                    _authenticationResult = null;
                    LoggedOut?.Invoke(this, EventArgs.Empty);
                    return string.Empty;
                }
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
                _authenticationResult = null;
                return true;
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
