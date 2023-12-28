using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class EmailObavijest
    {
        [Key]
        public int EmailID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumSlanja { get; set; }


        public int PoslodavacID { get; set; }
        [ForeignKey(nameof(PoslodavacID))]
        public Poslodavac Poslodavac { get; set; }



        [ForeignKey(nameof(Posloprimaoc))]
        public int PosloprimaocID { get; set; }
        public Posloprimaoc Posloprimaoc { get; set; }
    }
}
