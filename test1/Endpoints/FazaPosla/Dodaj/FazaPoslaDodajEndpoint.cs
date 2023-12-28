using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.FazaPosla.Dodaj
{
    [Route("FazaPosla-dodaj")]
    public class FazaPoslaDodajEndpoint : MyBaseEndpoint<FazaPoslaDodajRequest, FazaPoslaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public FazaPoslaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<FazaPoslaDodajResponse> Obradi([FromBody] FazaPoslaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.FazaPosla
            {

                Naziv = request.Naziv,
                Opis=request.Opis
            };
            _applicationDbContext.FazePoslova.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new FazaPoslaDodajResponse
            {
                FazaPoslaId = noviObj.FazaPoslaID
            };
        }
    }

}
