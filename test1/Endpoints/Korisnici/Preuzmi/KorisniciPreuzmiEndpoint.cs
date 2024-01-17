


using Microsoft.AspNetCore.Mvc;

using evojacu.Endpoints.Korisnici.Preuzmi;

using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Korisnici.Preuzmi

{

    [Route("korisnici-preuzmi")]

    public class KorisniciPreuzmiEndpoint : MyBaseEndpoint<KorisniciPreuzmiRequest, KorisniciPreuzmiResponse>

    {

        private readonly evojacuDBContext _applicationDbContext;

        public KorisniciPreuzmiEndpoint(evojacuDBContext applicationDbContext)

        {

            _applicationDbContext = applicationDbContext;

        }

        [HttpGet]

        public override async Task<KorisniciPreuzmiResponse> Obradi([FromQuery] KorisniciPreuzmiRequest request, CancellationToken cancellationToken = default)

        {


            var korisnici = _applicationDbContext.Korisnici.ToList();


            if (korisnici == null)

            {

                throw new Exception("Nema korisnika u bazi");

            }

            return new KorisniciPreuzmiResponse

            {

                Korisnici = korisnici

            };

        }

    }

}

