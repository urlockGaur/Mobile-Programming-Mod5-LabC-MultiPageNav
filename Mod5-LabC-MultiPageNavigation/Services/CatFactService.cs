using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Networking; // For Connectivity

namespace Mod5_LabC_MultiPageNavigation.Services
{
    public static class CatFactService
    {
        static readonly string BaseAddress = "https://catfact.ninja";
        static readonly string Url = $"{BaseAddress}/fact";

        static HttpClient client;

        static CatFactService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public static async Task<CatFact> GetRandomCatFactAsync()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                throw new Exception("No internet connection available.");

            var client = await GetClient();
            string result = await client.GetStringAsync(Url);

            return JsonSerializer.Deserialize<CatFact>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        private static Task<HttpClient> GetClient()
        {
            return Task.FromResult(client);
        }
    }

    public class CatFact
    {
        public string Fact { get; set; }
        public int Length { get; set; }
    }
}