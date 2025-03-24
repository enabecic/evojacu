using System;
using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zanimanje { get; set; }
        public string Adresa { get; set; }
        public string? Telefon { get; set; }

        // Skladištenje slike u Base64 formatu
        public byte[]? Slika { get; set; }
        public string? SlikaMimeType { get; set; } // Dodano za MIME tip

        // Dodato za verifikaciju
        public string? VerificationToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
