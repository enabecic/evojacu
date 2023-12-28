using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.RangLista.Update
{
    [Route("RangLista-update")]
    public class RangListaUpdateEndpoint : MyBaseEndpoint<RangListaUpdateRequest, RangListaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RangListaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<RangListaUpdateResponse> Obradi([FromBody]RangListaUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var rang=_applicationDbContext.RangListe.FirstOrDefault(x=>x.RangListaID==request.RangListaID);

            if (rang==null)
            {
                throw new Exception("Ne postoji rang lista sa ID = " + request.RangListaID);
            }

            rang.Pozicija=request.Pozicija;
            rang.BrojZadataka=request.BrojZadataka;
            rang.ProsjecnaOcjena=request.ProsjecnaOcjena;

            await _applicationDbContext.SaveChangesAsync();


            return new RangListaUpdateResponse
            {
                 RangListaID=rang.RangListaID
            };
        }
    }
}
