using System.ComponentModel.DataAnnotations;
namespace evojacu.Models
{
    public class FazaPosla
    {
        [Key]
        public int FazaPoslaID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

    }

}
