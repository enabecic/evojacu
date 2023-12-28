namespace evojacu.Endpoints.VrstaPlacanja.Dodaj
{
    public class VrstaPlacanjaDodajRequest
    {
        public string TipPlacanja { get; set; }
        public string? BrojKartice { get; set; }
        public DateTime DatumIsteka { get; set; }
    }
}
