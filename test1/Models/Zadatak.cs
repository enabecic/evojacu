using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Zadatak
    {
        public int ZadatakId { get; set; }

        [ForeignKey(nameof(Kategorija))]
        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get;set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string? Slika { get; set; }


        public int KorisnikId { get; set; }
        [ForeignKey(nameof(KorisnikId))]
        public Korisnik Korisnik { get; set; }
    }
}
