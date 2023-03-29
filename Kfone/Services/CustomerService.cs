using Kfone.Core.Helpers;
using Kfone.Core.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.System;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kfone.Services
{
    public static class CustomerService
    {
        public static List<Customer> customers = new List<Customer>();

        public static IDictionary<Roles, string> groupRoleMapping = new Dictionary<Roles, string>() {
            { Roles.AdminG, "groupId" },
            { Roles.SalesRep, "groupId" },
            { Roles.Marketing, "groupId" },
            { Roles.Customer, "groupId" },
        };

        private static IdentityMgtService _managementService => Singleton<IdentityMgtService>.Instance;

        public static void SetCustomer(Customer staff)
        {
            customers.Add(staff);
        }

        public static void DeleteCustomer(Customer staff)
        {
            customers.Remove(staff);
        }

        public static async Task AddCustomer(Customer staff)
        {
            string url = "https://api.asgardeo.io/t/kfoneteam2/scim2/Users";
            string[] scopes = { "internal_user_mgt_create" };
            string access_token = _managementService.GetAccessToken();

            string email = staff.email;
            string tier = staff.tier.ToString();

            string json = $@"{{
                    ""schemas"": [],
                    ""name"": {{
                        ""givenName"": ""{staff.name}"",
                    }},
                    ""userName"": ""DEFAULT/{email}"",
                    ""password"": ""Abc1234!"",
                    ""emails"": [
                        {{
                            ""value"": ""{email}"",
                            ""primary"": true
                        }}
                    ],
                    ""dob"" : ""{staff.dob}"",
                    ""profileUrl"": ""{staff.profilePic}"",
                    ""urn:scim:wso2:schema"": {{
                        ""askPassword"": true,
                        ""tier"": ""{tier.ToString()}""
                    }}
                }}
            ";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/scim+json"));

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string userId = jsonObj.id;
                    string username = jsonObj.userName;
                    //await AddUserToGroup(username, userId, groupRoleMapping[staff.roles]);
                }

            }
        }

        private static async Task AddUserToGroup(string username, string userID, string groupId)
        {

            string url = $"https://api.asgardeo.io/t/kfoneteam2/scim2/Groups/{groupId}";
            string[] scopes = { "internal_group_mgt_update" };
            string access_token = _managementService.GetAccessToken();

            var patchRequest = new
            {
                schemas = new[] { "urn:ietf:params:scim:api:messages:2.0:PatchOp" },
                Operations = new[]
                     {
                new
                {
                    op = "add",
                    value = new
                    {
                        members = new[]
                        {
                            new
                            {
                                display = username,
                                value = userID
                            }
                        }
                    }
                }
            }
            };

            string json = JsonConvert.SerializeObject(patchRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/scim+json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token); // Replace with the actual access token

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/scim+json");
                try
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    // Failed
                }
            }
        }
        
        private static async Task DeleteUser(string userID)
        {

            string url = $"https://api.asgardeo.io/t/kfoneteam2/scim2/Users/{userID}";
            string[] scopes = { "internal_user_mgt_delete" };
            string access_token = _managementService.GetAccessToken();


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/scim+json"));


                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string userId = jsonObj.id;
                    string username = jsonObj.userName;
                    await AddUserToGroup(username, userId, "groupId");
                }

            }
        }

        private static Tiers GetTier(string tier)
        {
            switch (tier)
            {
                case "Gold":
                    return Tiers.Gold;
                case "Silver":
                    return Tiers.Silver;
                case "Platinum":
                    return Tiers.Platinum;
                default:
                    return Tiers.Default;
            }
        }

        public static async Task<List<Customer>> GetUsers()
        {

            string url = $"https://api.asgardeo.io/t/kfoneteam2/scim2/Users?domain=DEFAULT";
            string[] scopes = { "internal_user_mgt_list" };
            string access_token = _managementService.GetAccessToken();

            Tiers tier;


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/scim+json"));


                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    customers.Clear();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    dynamic resources = jsonObj.Resources;
                    var sb = new StringBuilder(128);
                    try
                    {
                        foreach (dynamic item in resources)
                        {
                            var group = item?.groups?[0]?.display;

                            if (group != "DEFAULT/ApplicationAdmin" && group != "DEFAULT/SalesRep" && group != "DEFAULT/MarketingLead")
                            {
                                string userName = item.userName;
                                string userId = item.id;
                                string name = item?.name?.givenName;
                                string email = item?.emails?[0];
                                string profileUrl = item?.profileUrl;
                                string tierString = item?["urn:scim:wso2:schema"]?.tier ?? "";
                                tier = GetTier(tierString);
                                DateTimeOffset dob = item?.dob != null ? DateTimeOffset.Parse(item?.dob) : DateTimeOffset.MinValue;
                                customers.Add(new Customer
                                {
                                    name = name,
                                    email = email,
                                    tier = tier,
                                    dob = dob,
                                    profilePic = profileUrl ?? "https://cdn3.iconfinder.com/data/icons/web-design-and-development-2-6/512/87-1024.png",
                                    address = "20, PG, Colombo-03",
                                    contactNo = "063542635387"
                                });
                            }

                        }

                        return customers;
                    }
                    catch (Exception)
                    {
                        return customers;
                    }

                }
            }

            return customers;
        }
    }
}
