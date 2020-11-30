using Search.Fight.Applicacion.Model;
using System.Threading.Tasks;

namespace Search.Fight.Application.Service.Interface
{
    public interface ISearchEngine
    {
        string Name { get; }
        Task<SearchResponse> Search(string searchTerm);
    }
}