

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Posloprimaoc
    {
        public int PosloprimaocID { get; set; }
        //povezivanje sa korisnikom
        public int KorisnikId { get; set; }
        [ForeignKey(nameof(KorisnikId))]
        public Korisnik Korisnik { get; set; }
        public string Strucnost { get; set; }
    }
}
