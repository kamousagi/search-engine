using Search.Fight.Applicacion.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Fight.Application.Service.Interface
{
    public interface ISearcher
    {
        Task<List<SearchResult>> Search(string[] searchTerms);
    }
}