using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Korisnik.Preuzmi
{
    [Route("korisnik-preuzmi")]
    public class KorisnikPreuzmiEndpoint : MyBaseEndpoint<KorisnikPreuzmiRequest, KorisnikPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KorisnikPreuzmiResponse> Obradi([FromQuery] KorisnikPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            // Pretraži korisnike na osnovu email-a i lozinke
            var korisnik = await _applicationDbContext.Korisnici
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.Lozinka == request.Lozinka, cancellationToken);

            if (korisnik != null)
            {
                // Ako je korisnik pronađen, vrati podatke o korisniku
                var korisnici = new List<KorisnikPreuzmiResponseKorisnici>
                {
                    new KorisnikPreuzmiResponseKorisnici
                    {
                        Email = korisnik.Email,
                        Ime = korisnik.Ime,
                        Prezime = korisnik.Prezime,
                        Adresa = korisnik.Adresa,
                        Telefon = korisnik.Telefon,
                        KorisnikID = korisnik.KorisnikID,
                        Username = korisnik.Username,
                        Lozinka = korisnik.Lozinka,
                        Zanimanje = korisnik.Zanimanje
                    }
                };

                return new KorisnikPreuzmiResponse
                {
                    Korisnici = korisnici
                };
            }
            else
            {
                // Ako korisnik nije pronađen, vrati praznu listu ili obavijest
                return new KorisnikPreuzmiResponse
                {
                    Korisnici = new List<KorisnikPreuzmiResponseKorisnici>()
                };
            }
        }
    }
}
