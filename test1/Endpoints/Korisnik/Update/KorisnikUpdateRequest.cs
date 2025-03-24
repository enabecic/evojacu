using Microsoft.AspNetCore.Http;

namespace evojacu.Endpoints.Korisnik.Update
{
    public class KorisnikUpdateRequest
    {
       
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Lozinka { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Zanimanje { get; set; }
        public string? Adresa { get; set; }
        public string? Telefon { get; set; }
  
    }
}
