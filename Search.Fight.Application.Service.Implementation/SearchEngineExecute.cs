using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Fight.Application.Service.Implementation
{
    public class SearchEngineExecute : ISearcher
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        public SearchEngineExecute(IEnumerable<ISearchEngine> searchEngines)
        {
            _searchEngines = searchEngines;
        }

        public async Task<List<SearchResult>> Search(string[] searchTerms)
        {
            var searchResults = new List<SearchResult>();

            foreach (var searchTerm in searchTerms)
            {
                foreach (var searchEngine in _searchEngines)
                {
                    var searchReponse = await searchEngine.Search(searchTerm);

                    if (searchReponse.Success)
                    {
                        var searchResult = new SearchResult()
                        {
                            SeachEngine = searchEngine.Name,
                            SearchTerm = searchTerm,
                            TotalResults = searchReponse.TotalResults.Value
                        };
                        searchResults.Add(searchResult);
                    }

                }
            }

            return searchResults;
        }
    }
}