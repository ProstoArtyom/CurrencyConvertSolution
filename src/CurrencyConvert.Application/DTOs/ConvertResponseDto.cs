namespace CurrencyConvert.Application.DTOs
{
    public class ConvertResponseDto
    {
        public string From { get; set; } 
        public string To { get; set; }
        public decimal Amount { get; set; }
        public string Bank { get; set; }
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
    }
}
