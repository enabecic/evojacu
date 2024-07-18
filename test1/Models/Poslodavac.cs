

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Poslodavac
    {
        [Key]
        public int PoslodavacID { get; set; }

        // Povezivanje s korisnikom
        public int KorisnikId { get; set; }
        [ForeignKey(nameof(KorisnikId))]
        public Korisnik Korisnik { get; set; }

        //[ForeignKey("Korisnik")]
        //public int KorisnikID { get; set; }

        //public Korisnik Korisnik { get; set; }

        public string? NazivKompanije { get; set; }

    }


}
