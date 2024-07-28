namespace evojacu.Endpoints.Kategorija.Update
{
    public class KategorijaUpdateRequest
    {

        public int KategorijaID { get; set; }
        public string Naziv { get; set; }
        public string? Slika_base64_format { get; set; } 
    }
}
