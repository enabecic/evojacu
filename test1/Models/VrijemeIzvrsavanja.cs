using System.ComponentModel.DataAnnotations;
namespace evojacu.Models
{
    public class VrijemeIzvrsavanja
    {
            [Key]
            public int VrijemeIzvrsavanjaID { get; set; }
            //public DateTime PocetakVremena { get; set; }
            public DateTime KrajVremena { get; set; }

          
        

    }
}
