using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class Obaveza
    {
        [Key]
        public int ObavezaID { get; set; }

        //povezivanje sa posloprimaoc

        [ForeignKey(nameof(Posloprimaoc))]

        public int PosloprimaocID { get; set; }
        public Posloprimaoc Posloprimaoc { get; set; }

       
        public string Opis { get; set; }
        public DateTime DatumRokaIzvrsenja { get; set; }

        //povezivanje sa prioritet
        public int PrioritetID { get; set; }
        [ForeignKey(nameof(PrioritetID))]
        public Prioritet Prioritet { get; set; }

    }
}
