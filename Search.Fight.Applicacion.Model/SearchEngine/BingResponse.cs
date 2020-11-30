namespace Search.Fight.Applicacion.Model
{
    public class BingResponse
    {
        public webPages webPages { get; set; }
    }

    public class webPages
    {
        public long totalEstimatedMatches { get; set; }
    }

}