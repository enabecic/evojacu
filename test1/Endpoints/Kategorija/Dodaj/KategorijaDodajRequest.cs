namespace evojacu.Endpoints.Kategorija.Dodaj
{
    public class KategorijaDodajRequest
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string? Slika_base64_format { get; set; }

    }
}
