using Microsoft.AspNetCore.Mvc;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace evojacu.Endpoints.Korisnik.Preuzmi
{
    [Route("korisnik-preuzmi")]
    public class KorisnikPreuzmiEndpoint : ControllerBase
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("{korisnikId}")] // Prima ID iz URL-a
        public async Task<ActionResult<KorisnikPreuzmiResponse>> Obradi(
            [FromRoute] int korisnikId,
            CancellationToken cancellationToken = default)
        {
            var korisnik = await _applicationDbContext.Korisnici
                .FirstOrDefaultAsync(x => x.KorisnikID == korisnikId, cancellationToken);

            if (korisnik == null)
            {
                return NotFound(new { Poruka = "Korisnik nije pronađen." });
            }

            var mimeType = korisnik.SlikaMimeType ?? "image/jpeg"; // Ako nije postavljen, postavi podrazumevani tip
            var slika = korisnik.Slika != null ? $"data:{mimeType};base64,{Convert.ToBase64String(korisnik.Slika)}" : null;

            var korisnici = new List<KorisnikPreuzmiResponseKorisnici>
            {
                new KorisnikPreuzmiResponseKorisnici
                {
                    KorisnikID = korisnik.KorisnikID,
                    Username = korisnik.Username,
                    Email = korisnik.Email,
                    Lozinka = korisnik.Lozinka, // Možda ovo ne treba slati iz sigurnosnih razloga?
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Zanimanje = korisnik.Zanimanje,
                    Adresa = korisnik.Adresa,
                    Telefon = korisnik.Telefon,
                    Slika = slika,
                    SlikaMimeType = korisnik.SlikaMimeType
                }
            };

            return Ok(new KorisnikPreuzmiResponse
            {
                Korisnici = korisnici
            });
        }
    }

}
