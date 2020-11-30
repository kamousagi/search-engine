using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Interface;

namespace Search.Fight.Application.Service.Implementation.SearchEngine
{
    //https://docs.microsoft.com/en-us/bing/search-apis/bing-web-search/quickstarts/rest/csharp
    public class BingEngine : ISearchEngine
    {
        public string Name => "MSNSearch";

        private readonly HttpClient _httpClient;

        public BingEngine(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SearchResponse> Search(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"search?q={Uri.EscapeDataString(searchTerm)}+mkt=en-us&textDecorations=true");

            var result = new SearchResponse();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<BingResponse>(content);

                result = new SearchResponse
                {
                    Success = true,
                    TotalResults = tasks.webPages.totalEstimatedMatches
                };
            }
            else
            {
                result = new SearchResponse
                {
                    Success = false
                };
                //response.EnsureSuccessStatusCode();
            }

            return result;
        }

    }

}