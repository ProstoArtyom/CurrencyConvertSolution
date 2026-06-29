namespace CurrencyConvert.Infrastructure.Configuration
{
    public class BankSettings
    {
        public static string SectionName => "BankSettings";

        public BankEndpoint Nbrb { get; set; } = new();
        public BankEndpoint Alfabank { get; set; } = new();
    }

    public class BankEndpoint
    {
        public string Name { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}
