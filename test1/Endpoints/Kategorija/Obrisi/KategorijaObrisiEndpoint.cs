using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Kategorija.Obrisi
{
    [Route("Kategorija-obrisi")]
    public class KategorijaObrisiEndpoint : MyBaseEndpoint<KategorijaObrisiRequest, KategorijaObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KategorijaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpDelete]
        public override async Task<KategorijaObrisiResponse> Obradi([FromQuery]KategorijaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var kategorija=_applicationDbContext.Kategorije.FirstOrDefault(x=>x.KategorijaID==request.KategorijaID);

            if(kategorija==null)
            {
                throw new Exception("Ne postoji kategorija sa ID= " + request.KategorijaID);
            }

            _applicationDbContext.Remove(kategorija);
            await _applicationDbContext.SaveChangesAsync();

            return new KategorijaObrisiResponse
            {

            };
        }
    }
}
