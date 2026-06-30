namespace CurrencyConvert.Application.DTOs
{
    public class RateResultDto
    {
        public string Currency { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public string Bank { get; set; }
    }
}
