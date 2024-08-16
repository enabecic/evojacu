

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using evojacu.Models;

namespace evojacu.Models
{
    public class Posao
    {
            [Key]
            public int ZadatakID{ get; set; }
           
        // Povezivanje s VrijemeIzvrsavanja
        public int VrijemeIzvrsavanjaId { get; set; }
        [ForeignKey(nameof(VrijemeIzvrsavanjaId))]
        public VrijemeIzvrsavanja VrijemeIzvrsavanja { get; set; }


        //Povezivanje sa Grad
        public int GradId { get; set; }
        [ForeignKey(nameof(GradId))]
        public Grad Grad { get; set; }

        //Povezivanje sa FazaPosla
        public int FazaPoslaId { get; set; }
        [ForeignKey(nameof(FazaPoslaId))]
        public FazaPosla FazaPosla { get; set; }

        //Povezivanje sa Transakcija
        //public int TransakcijaId { get; set; }
        //[ForeignKey(nameof(TransakcijaId))]
        //public Transakcija Transakcija { get; set; }

        //prebacila sam u Transakciju da bude PosaoID
        public string OpisPosla { get; set; }
       // public bool JePonuda { get; set; }

        public string Adresa { get; set; }

        [ForeignKey(nameof(Poslodavac))]
        public int PoslodavacID { get; set; }
        public Poslodavac Poslodavac { get; set; }


        [ForeignKey(nameof(Zadatak))]
        public int ZadatakStraniID { get; set; }
        public Zadatak Zadatak { get; set; }


        public DateTime DatumObjave { get; set; }

        public float Cijena { get; set; }
        public bool UkljucenGPS { get; set; }

        public bool jeOdabran { get; set; }=false;

    }
}
