using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;

using Kfone.Core.Helpers;
using Kfone.Core.Models;
using Newtonsoft.Json;
using Windows.Media.Protection.PlayReady;
using Windows.UI.Xaml;

namespace Kfone.Services
{
    public class IdentityMgtService
    {

        private readonly string _clientId = "lp79hGwq602Hx7jjhDuTlv08EOMa";
        private readonly string _clientSecrect = "MfyTzRqPhHROq4MIQ2l67rs_woMa";

        private string _access_token;
        private DateTime _expire_time;

        public async void InitializeWithClientCredentials()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecrect}")));

                var content = new FormUrlEncodedContent(new[] {

                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>( "scope", "SYSTEM")
                });

                HttpResponseMessage response = await client.PostAsync("https://api.asgardeo.io/t/kfoneteam2/oauth2/token", content);
                string resBody = await response.Content.ReadAsStringAsync();
                dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(resBody);
                _access_token = jsonObj.access_token;
                int expires_in = jsonObj.expires_in;
                DateTime currentTime = DateTime.Now;
                _expire_time = currentTime.AddSeconds(expires_in);

            }
        }

        public string GetAccessToken()
        {
            return _access_token;
        }

        public DateTime GetExpireTime() {

            return _expire_time;
        }

    }
}
