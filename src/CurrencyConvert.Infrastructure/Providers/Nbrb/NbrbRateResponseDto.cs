namespace CurrencyConvert.Infrastructure.Providers.Nbrb
{
    public class NbrbRateResponseDto
    {
        public string? Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public decimal Cur_OfficialRate { get; set; }
    }
}
