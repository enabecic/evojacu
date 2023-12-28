using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class Kategorija
    {
        [Key]
        public int KategorijaID { get; set; }
        public string Naziv { get; set; }
    }
}
