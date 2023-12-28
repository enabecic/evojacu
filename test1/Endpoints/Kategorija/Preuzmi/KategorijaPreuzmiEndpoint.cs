using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Kategorija.Preuzmi
{

    [Route("Kategorija-preuzmi")]
    public class KategorijaPreuzmiEndpoint : MyBaseEndpoint<KategorijaPreuzmiRequest, KategorijaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KategorijaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public override async Task<KategorijaPreuzmiResponse> Obradi([FromQuery]KategorijaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var kategorije = await _applicationDbContext.Kategorije.Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new KategorijaPreuzmiResponseKategorija()
                {
                    KategorijaID=x.KategorijaID,
                    Naziv=x.Naziv
                }).ToListAsync(cancellationToken: cancellationToken);

            return new KategorijaPreuzmiResponse
            {
                Kategorije=kategorije
            };
        }
    }
}
