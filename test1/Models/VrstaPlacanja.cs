
using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class VrstaPlacanja
    {
        [Key]
        public int VrstaPlacanjaID { get; set; }
        public string TipPlacanja { get; set; } 
        public string? BrojKartice { get; set; }
        public DateTime DatumIsteka { get; set; }
      //  public List<Transakcija> Transakcije { get; set; } = new List<Transakcija>();
        // Dodajte dodatne atribute ako su potrebni
    }
}
