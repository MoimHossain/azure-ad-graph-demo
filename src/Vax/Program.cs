

using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Vax
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user ID: ");
            var userId = Console.ReadLine(); //"9c5918c7-ef03-4059-a49e-aa6e6d761423";
            Console.WriteLine("U/M ? ");
            var sign = Console.ReadLine(); 

            if("U".Equals(sign, StringComparison.OrdinalIgnoreCase))
            {
                var x = GetUserAsync(userId).Result;
            }
            else
            {
                var y = GetManagerAsync(userId).Result;
            }
        }

        private async static Task<AdUser> GetUserAsync(string userId)
        {
            var tokenInfo = await new CliToken().GetAdGraphTokenAsync();
            var uri = $"https://graph.windows.net/{tokenInfo.TenantId}/users/{userId}?api-version=1.6";
            var result = await client.SendAsync(CreateHttpRequest(tokenInfo, uri));
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<AdUser>(content);
        }

        private async static Task<AdUser> GetManagerAsync(string userId)
        {
            var tokenInfo = await new CliToken().GetAdGraphTokenAsync();
            var uri = $"https://graph.windows.net/{tokenInfo.TenantId}/users/{userId}/$links/manager?api-version=1.6";
            var result = await client.SendAsync(CreateHttpRequest(tokenInfo, uri));
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AdUser>(content);
            }
            else
            {
                return null;
            }
        }

        private static HttpRequestMessage CreateHttpRequest(TokenBlock tokenInfo, string uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };
            request.Headers.AcceptCharset.Clear();
            request.Headers.Accept.Clear();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
            return request;
        }

 

        //private static void LoadUsers()
        //{
        //    var x = new CliToken().GetTokensAsync().Result;

        //    var tokenInfo = x.First(tc => tc.Resource.Equals("https://graph.windows.net/", StringComparison.OrdinalIgnoreCase));

        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri($"https://graph.windows.net/{tokenInfo.TenantId}/users?api-version=1.6")
        //    };
        //    request.Headers.AcceptCharset.Clear();
        //    request.Headers.Accept.Clear();
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.AccessToken);
        //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));

        //    var result = client.SendAsync(request).Result;

        //    var response = result.Content.ReadAsStringAsync().Result;

        //    var allUsers = JsonConvert.DeserializeObject<AdUserCollection>(response);

        //    Console.WriteLine("Hello World!");
        //}
    }
}
