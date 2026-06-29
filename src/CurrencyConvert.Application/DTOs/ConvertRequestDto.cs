namespace CurrencyConvert.Application.DTOs
{
    public class ConvertRequestDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
        public string Bank { get; set; }
    }
}
