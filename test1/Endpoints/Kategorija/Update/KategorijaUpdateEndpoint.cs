using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Kategorija.Update
{
    [Route("Kategorija-update")]

    public class KategorijaUpdateEndpoint : MyBaseEndpoint<KategorijaUpdateRequest, KategorijaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KategorijaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<KategorijaUpdateResponse> Obradi([FromBody]KategorijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var kategorije=_applicationDbContext.Kategorije.FirstOrDefault(x=>x.KategorijaID==request.KategorijaID);

            if (kategorije == null)
            {
                throw new Exception("Ne postoji kategorija sa ID= " + request.KategorijaID);
            }

            kategorije.Naziv = request.Naziv;

            await _applicationDbContext.SaveChangesAsync();

            return new KategorijaUpdateResponse
            {
                 KategorijaID=kategorije.KategorijaID
            };
        }
    }
}
