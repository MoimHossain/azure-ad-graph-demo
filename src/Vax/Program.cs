

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

            AdUser user;
            if ("U".Equals(sign, StringComparison.OrdinalIgnoreCase))
            {
                user = GetUserAsync(userId).Result;

            }
            else
            {
                user = GetManagerAsync(userId).Result;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"User Mail Nick: {user.MailNickname}");
            Console.ResetColor();
        }

        private async static Task<AdUser> GetUserAsync(string userId)
        {
            var tokenInfo = await new CliToken().GetAdGraphTokenAsync();
            var uri = $"https://graph.windows.net/{tokenInfo.TenantId}/users/{userId}?api-version=1.6";
            var result = await client.SendAsync(CreateHttpRequest(tokenInfo, uri));
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdUser>(content);
        }

        private async static Task<AdUser> GetManagerAsync(string userId)
        {
            var tokenInfo = await new CliToken().GetAdGraphTokenAsync();
            var uri = $"https://graph.windows.net/{tokenInfo.TenantId}/users/{userId}/$links/manager?api-version=1.6";
            var result = await client.SendAsync(CreateHttpRequest(tokenInfo, uri));
            var content = await result.Content.ReadAsStringAsync();
            var mgrRef = JsonConvert.DeserializeObject<AdManagerReference>(content);
            return await GetUserAsync(mgrRef.ManagerId);
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
    }
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