namespace evojacu.Endpoints.Korisnik.Preuzmi
{
    public class KorisnikPreuzmiResponse
    {
        public List<KorisnikPreuzmiResponseKorisnici> Korisnici { get; set; }
    }

    public class KorisnikPreuzmiResponseKorisnici
    {
        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zanimanje { get; set; }
        public string Adresa { get; set; }
        public string? Telefon { get; set; }

        public string? Slika { get; set; }
        public string? SlikaMimeType { get; set; }
    }
}
