namespace MarketAnalysisWebApi.Models.Configuration
{
    public class EmailServerData
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}
