using System.ComponentModel.DataAnnotations;

namespace evojacu.Models
{
    public class Korisnik
    {
       
        
            [Key]
            public int KorisnikID { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Lozinka { get; set; }

          

          


       

   
}
}
