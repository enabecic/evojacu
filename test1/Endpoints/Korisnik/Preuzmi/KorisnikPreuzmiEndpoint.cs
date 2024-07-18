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

            var korisnici = await _applicationDbContext.Korisnici
                .Select(x => new KorisnikPreuzmiResponseKorisnici()
                {
                    Email = x.Email,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    Telefon = x.Telefon,
                    KorisnikID = x.KorisnikID,
                    Username = x.Username,
                    Lozinka = x.Lozinka,
                    Zanimanje = x.Zanimanje
                }).ToListAsync(cancellationToken: cancellationToken);

            return new KorisnikPreuzmiResponse
            {
                Korisnici = korisnici
            };

            //var korisnici = _applicationDbContext.Korisnici.ToList();

            //var korisnik = korisnici.FirstOrDefault(k => k.Email == request.Email);

            //if (korisnik != null)
            //{
            //    Console.WriteLine("Korisnik je pronađen!");

            //    return new KorisnikPreuzmiResponse
            //    {
            //        Id = korisnik.KorisnikID
            //    };


            //}
            //else
            //{
            //    Console.WriteLine("Korisnik nije pronađen!");
            //    throw new Exception("Ne postoji email ili lozinka");
            //}


        }


    }
}
