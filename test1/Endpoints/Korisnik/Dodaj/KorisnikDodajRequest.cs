namespace evojacu.Endpoints.Korisnik.Dodaj
{
    public class KorisnikDodajRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zanimanje { get; set; }
        public string Adresa { get; set; }
        public string? Telefon { get; set; }
    }
}
