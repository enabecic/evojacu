using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class Gost
    {
        [Key]
        public int GostID { get; set; }
        public int BrojPosjeta { get; set; }
    }
}
