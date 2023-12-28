using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.RangLista.Dodaj
{
    [Route("RangLista-dodaj")]
    public class RangListaDodajEndpoint : MyBaseEndpoint<RangListaDodajRequest, RangListaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RangListaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<RangListaDodajResponse> Obradi([FromBody]RangListaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.RangLista
            {
                KorisnikID= request.KorisnikID,
                Pozicija=request.Pozicija,
                BrojZadataka=request.BrojZadataka,
                ProsjecnaOcjena=request.ProsjecnaOcjena
                 
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new RangListaDodajResponse
            {
                 RangListaID=noviObj.RangListaID
            };
        }
    }
}
