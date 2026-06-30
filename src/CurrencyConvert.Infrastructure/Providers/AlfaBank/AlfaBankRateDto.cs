namespace CurrencyConvert.Infrastructure.Providers.AlfaBank
{
    public class AlfaBankRateDto
    {
        public decimal SellRate { get; set; }
        public string SellIso { get; set; }
        public decimal BuyRate { get; set; }
        public string BuyIso { get; set; }
        public int Quantity { get; set; }
    }
}
