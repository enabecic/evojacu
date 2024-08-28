using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Recenzija
    {
        [Key]
        public int RecenzijaID { get; set; }

        [ForeignKey(nameof(Posao))]
        public int PosaoID { get; set; }
        public Posao Posao { get; set; }

        [ForeignKey(nameof(Posloprimaoc))]
        public int PosloprimaocID { get; set; }
        public Posloprimaoc Posloprimaoc { get; set; }

        public string Komentar { get; set; }
   

    }
}
