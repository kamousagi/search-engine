namespace Search.Fight.Applicacion.Model
{
    public class Configuration
    {
        public GoogleConfiguration GoogleConfiguration { get; set; }
        public BingConfiguration BingConfiguration { get; set; }
    }

    public class GoogleConfiguration
    {
        public string Uri { get; set; }
        public string Key { get; set; }
        public string Custom { get; set; }
    }

    public class BingConfiguration
    {
        public string Uri { get; set; }
        public string OcpApimSubscriptionKey { get; set; }
    }
}