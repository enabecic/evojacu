using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.RangLista.Obrisi
{
    [Route("RangLista-obrisi")]
    public class RangListaObrisiEndpoint : MyBaseEndpoint<RangListaObrisiRequest, RangListaObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RangListaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }




        [HttpDelete]
        public override async Task<RangListaObrisiResponse> Obradi([FromQuery]RangListaObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var rang= _applicationDbContext.RangListe.FirstOrDefault(x=>x.RangListaID==request.RangListaID);


            if (rang==null) {
                throw new Exception("Ne postoji rang lista sa id = " + request.RangListaID);
            }

            _applicationDbContext.Remove(rang);
            await _applicationDbContext.SaveChangesAsync();

            return new RangListaObrisiResponse
            {

            };
        }
    }
}
