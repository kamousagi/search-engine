using Newtonsoft.Json;
using Search.Fight.Applicacion.Model;
using Search.Fight.Applicacion.Model.SearchEngine;
using Search.Fight.Application.Service.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace Search.Fight.Application.Service.Implementation.SearchEngine
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly HttpClient _httpClient;

        public GoogleSearchEngine(HttpClient client)
        {
            _httpClient = client;
        }

        public string Name => "Google";

        public async Task<SearchResponse> Search(string searchTerm)
        {
            string key = "AIzaSyA3eWIYlfLzd5Fj9jsZXPwzANzbXeP-WC4";
            string custom = "b15c974d12e0ac7cd";
            var response = await _httpClient.GetAsync($"v1?key={key}&cx={custom}&q={searchTerm}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<GoogleResponse>(content);

            var result = new SearchResponse
            {
                Success = true,
                TotalResults = tasks.searchInformation.totalResults
            };

            return result;
        }
    }
}