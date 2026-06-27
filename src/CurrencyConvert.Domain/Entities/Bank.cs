namespace CurrencyConvert.Domain.Entities
{
    public class Bank
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ApiBaseUrl { get; set; } = string.Empty;
    }
}