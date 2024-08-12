using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evojacu.Models
{
    public class OdabraniPosao
    {
        [Key]
        public int OdabraniPosaoID { get; set; }

        [ForeignKey(nameof(Posloprimaoc))]
        public int PosloprimaocID { get; set; }
        public Posloprimaoc Posloprimaoc { get; set; }

        [ForeignKey(nameof(Posao))]
        public int PosaoID { get; set; }
        public Posao Posao { get; set; }

        public DateTime DatumOdabira { get; set; }

    }
}
