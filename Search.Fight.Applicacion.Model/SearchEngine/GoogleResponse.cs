namespace Search.Fight.Applicacion.Model.SearchEngine
{
    public class GoogleResponse
    {
        public SearchInformation searchInformation { get; set; }
    }

    public class SearchInformation
    {
        public long totalResults { get; set; }
    }
}