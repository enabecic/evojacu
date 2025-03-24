namespace evojacu.Endpoints.Zadatak.Dodaj
{
    public class ZadatakDodajRequest
    {
        public int ID { get; set; }
        public int KategorijaID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string? Slika_base64_format { get; set; }

        public int KorisnikID { get; set; }
    }
}
