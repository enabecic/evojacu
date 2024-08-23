namespace evojacu.Endpoints.Zadatak.Preuzmi
{
    public class ZadatakPreuzmiRequest
    {
        public string? Naziv { get; set; }
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 4;
    }
}
