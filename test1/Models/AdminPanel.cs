using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class AdminPanel
    {
        [Key]
        public int AdminID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

    }
}
