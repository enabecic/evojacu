using Microsoft.AspNetCore.Mvc;
using evojacu.Models;
using System.Linq;
using evojacu.Endpoints.Korisnik.Update;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace evojacu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikController(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPut("{korisnikID}")]
        public ActionResult UpdateKorisnik(int korisnikID, [FromBody] KorisnikUpdateRequest podaci)
        {
            var korisnik = _applicationDbContext.Korisnici
                .Where(x => x.KorisnikID == korisnikID)
                .FirstOrDefault();

            if (korisnik == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(podaci.Ime))
                korisnik.Ime = podaci.Ime;

            if (!string.IsNullOrEmpty(podaci.Prezime))
                korisnik.Prezime = podaci.Prezime;

            if (!string.IsNullOrEmpty(podaci.Zanimanje))
                korisnik.Zanimanje = podaci.Zanimanje;

            if (!string.IsNullOrEmpty(podaci.Adresa))
                korisnik.Adresa = podaci.Adresa;

            if (!string.IsNullOrEmpty(podaci.Telefon))
                korisnik.Telefon = podaci.Telefon;



            _applicationDbContext.SaveChanges();

            string slikaBase64 = null;
            if (korisnik.Slika != null)
            {
                slikaBase64 = $"data:{korisnik.SlikaMimeType};base64,{korisnik.Slika}";
            }

            return Ok(new
            {
                korisnik.KorisnikID,
                korisnik.Ime,
                korisnik.Prezime,
                korisnik.Zanimanje,
                korisnik.Adresa,
                korisnik.Telefon,
                Slika = slikaBase64
            });
        }
    }
}
