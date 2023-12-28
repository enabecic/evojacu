using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class Prioritet
    {
        [Key]
        public int PrioritetID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

    }
}
