using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class RangLista
    {
        [Key]
        public int RangListaID { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get;set; }
        public Korisnik Korisnik { get; set; }

        public int Pozicija { get; set; }
        public int BrojZadataka { get; set; }
        public double ProsjecnaOcjena { get; set; }
    }
}
