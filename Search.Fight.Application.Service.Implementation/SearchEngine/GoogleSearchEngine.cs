using Newtonsoft.Json;
using Search.Fight.Applicacion.Model;
using Search.Fight.Applicacion.Model.SearchEngine;
using Search.Fight.Application.Service.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace Search.Fight.Application.Service.Implementation.SearchEngine
{
    //https://developers.google.com/custom-search/v1/using_rest
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

            var result = new SearchResponse();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<GoogleResponse>(content);

                result = new SearchResponse
                {
                    Success = true,
                    TotalResults = tasks.searchInformation.totalResults
                };
            }
            else
            {
                result = new SearchResponse{
                    Success = false
                };
                //response.EnsureSuccessStatusCode();
            }

            return result;
        }

    }

}