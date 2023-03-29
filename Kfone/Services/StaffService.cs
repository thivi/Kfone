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

namespace Kfone.Services
{
    public static class StaffService
    {
        public static List<Staff> staffs = new List<Staff>(new Staff[]
        {
            new Staff{name="John Doe", address="20, PG, Colombo-03", contactno="74373876847", dob=DateTime.Parse("03/03/1995"), joinedDate=DateTime.Parse("03/29/2023"), profilePic="https://th.bing.com/th/id/R.569493641bff31b6ee9a484586487b10?rik=KsmvdhoyrlKC7g&pid=ImgRaw&r=0", roles=Roles.Marketing}
        });

        public static IDictionary<Roles, string> groupRoleMapping = new Dictionary<Roles, string>() {
            { Roles.AdminG, "groupId" },
            { Roles.SalesRep, "groupId" },
            { Roles.Marketing, "groupId" },
            { Roles.Customer, "groupId" },
        };

        private static IdentityService _identityService => Singleton<IdentityService>.Instance;

        public static List<Staff> GetStaffs()
        {
            return staffs;
        }

        public static void SetStaff(Staff staff)
        {
            staffs.Add(staff);
        }

        public static void DeleteStaff(Staff staff)
        {
            staffs.Remove(staff);
        }

        private static async Task AddStaffUser(Staff staff)
        {

            string url = "https://api.asgardeo.io/t/kfoneteam2/scim2/Users";
            string[] scopes = { "internal_user_mgt_create" };
            string access_token = await _identityService.GetAccessTokenAsync(scopes);

            string email = "shshi@gmail.com";
            string tier = "gold";

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
                    ""tier"": ""{tier}"",
                    ""dob"" : ""{staff.dob}"",
                    ""photourl"": ""{staff.profilePic}"",
                    ""urn:scim:wso2:schema"": {{
                        ""askPassword"": true
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
                    await AddUserToGroup(username, userId, groupRoleMapping[staff.roles]);
                }

            }
        }

        private static async Task AddUserToGroup(string username, string userID, string groupId)
        {

            string url = $"https://api.asgardeo.io/t/kfoneteam2/scim2/Groups/{groupId}";
            string[] scopes = { "internal_group_mgt_update" };
            string access_token = await _identityService.GetAccessTokenAsync(scopes);

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
            string access_token = await _identityService.GetAccessTokenAsync(scopes);


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

        private static async Task GetAllUser()
        {

            string url = $"https://api.asgardeo.io/t/kfoneteam2/scim2/Users?domain=DEFAULT";
            string[] scopes = { "internal_user_mgt_list" };
            string access_token = await _identityService.GetAccessTokenAsync(scopes);


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/scim+json"));


                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    dynamic resources = jsonObj.Resources;
                    var sb = new StringBuilder(128);
                    IDictionary<string, string> users = new Dictionary<string, string>();

                    foreach (dynamic item in resources)
                    {
                        string userName = item.userName;
                        string userId = item.id;
                        users.Add(userId, userName);
                    }
                }

            }
        }
    }
}
