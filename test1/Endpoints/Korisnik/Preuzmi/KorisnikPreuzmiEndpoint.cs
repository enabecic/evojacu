using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;


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
            var korisnici = _applicationDbContext.Korisnici.ToList();

            var korisnik = korisnici.FirstOrDefault(k => k.Email == request.Email);

            if (korisnik != null)
            {
                Console.WriteLine("Korisnik je pronađen!");

                return new KorisnikPreuzmiResponse
                {
                    Id = korisnik.KorisnikID
                };


            }
            else
            {
                Console.WriteLine("Korisnik nije pronađen!");
                throw new Exception("Ne postoji email ili lozinka");
            }


        }


    }
}
