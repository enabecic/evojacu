namespace evojacu.Endpoints.Zadatak.Update
{
    public class ZadatakUpdateRequest
    {
        public int ZadatakId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string? Slika_base64_format { get; set; }
    }
}
