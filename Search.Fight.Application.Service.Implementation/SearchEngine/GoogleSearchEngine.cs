using Newtonsoft.Json;
using Search.Fight.Applicacion.Model;
using Search.Fight.Applicacion.Model.SearchEngine;
using Search.Fight.Application.Service.Interface;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;

namespace Search.Fight.Application.Service.Implementation.SearchEngine
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly HttpClient _httpClient;
        private readonly Configuration _configuracion;

        public GoogleSearchEngine(
            HttpClient client,
            IOptions<Configuration> configuration
        )
        {
            _httpClient = client;
            _configuracion = configuration.Value;
        }

        public string Name => "Google";

        public async Task<SearchResponse> Search(string searchTerm)
        {
            string key = _configuracion.GoogleConfiguration.Key;
            string custom = _configuracion.GoogleConfiguration.Custom;
            string searchTermEscape = Uri.EscapeDataString(searchTerm);

            string uri = $"v1?key={key}&cx={custom}&q={searchTermEscape}";

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
                result = new SearchResponse
                {
                    Success = false
                };
            }

            return result;
        }

    }

}