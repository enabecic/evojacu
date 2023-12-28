
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Transakcija
    {
        [Key]
        public int TransakcijaID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Iznos { get; set; }
        public DateTime VrijemeTransakcije { get; set; }

        //Spajanje sa VrstomPlacanja
        public int NacinPlacanjaId { get; set; }
        [ForeignKey(nameof(NacinPlacanjaId))]
        public VrstaPlacanja VrstaPlacanja { get; set; }

        // Povezivanje sa klasom StanjePlacanja
        public int StanjePlacanjaId { get; set; }
        [ForeignKey(nameof(StanjePlacanjaId))]
        public StanjePlacanja StanjePlacanja { get; set; } 

        public string Opis { get; set; }

        //Spajanje sa Posao
        public int PosaoID { get; set; }
        [ForeignKey(nameof(PosaoID))]
        public Posao Posao { get; set; }



        //Spajanje sa Poslodavaoc da se zna ko je izvrsio isplatu
        public int PoslodavacID { get; set; }
        [ForeignKey(nameof(PoslodavacID))]
        public Poslodavac Poslodavac { get; set; }

    }
}
