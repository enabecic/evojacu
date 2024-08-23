namespace evojacu.Endpoints.Kategorija.Preuzmi
{
    public class KategorijaPreuzmiRequest
    {
        public string? Naziv { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
