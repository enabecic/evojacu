using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.RangLista.Preuzmi
{
    [Route("RangLista-preuzmi")]
    public class RangListaPreuzmiEndpoint : MyBaseEndpoint<RangListaPreuzmiRequest, RangListaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RangListaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        [HttpGet]
        public override async Task<RangListaPreuzmiResponse> Obradi([FromQuery]RangListaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var rangovi = await _applicationDbContext.RangListe.Where(x => request.Username == null || x.Korisnik.Username.ToLower().StartsWith(request.Username.ToLower()))
                .Select(x => new RangListaPreuzmiResponseRang()
                {
                     RangListaID= x.RangListaID,
                     KorisnikID= x.KorisnikID,
                     Username=x.Korisnik.Username,
                     Email=x.Korisnik.Email,
                     Lozinka=x.Korisnik.Lozinka,
                     Pozicija=x.Pozicija,
                     BrojZadataka=x.BrojZadataka,
                     ProsjecnaOcjena=x.ProsjecnaOcjena
                }).ToListAsync(cancellationToken:cancellationToken);


            return new RangListaPreuzmiResponse
            {
                 Rangovi= rangovi
            };
        }
    }
}
