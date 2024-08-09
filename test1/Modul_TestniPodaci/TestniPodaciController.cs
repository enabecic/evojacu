using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Modul_TestniPodaci
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly evojacuDBContext _dbContext;   

        public TestniPodaciController(evojacuDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("FazaPosla", _dbContext.FazePoslova.Count());
            

            return Ok(data);
        }


        [HttpPost]
        public ActionResult Generisi()
        {
            var fazePoslova = new List<FazaPosla>();
            

            fazePoslova.Add(new FazaPosla { Opis = "Posao je aktivan", Naziv = "Aktivan" });
            fazePoslova.Add(new FazaPosla { Opis = "Posao nije aktivan", Naziv = "Neaktivan"});
            


            

            _dbContext.AddRange(fazePoslova);
            
            _dbContext.SaveChanges();

            return Count();
        }
    }
}
