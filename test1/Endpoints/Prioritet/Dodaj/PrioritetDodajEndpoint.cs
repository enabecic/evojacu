using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Prioritet.Dodaj
{
    [Route("Prioritet-dodaj")]
    public class PrioritetDodajEndpoint : MyBaseEndpoint<PrioritetDodajRequest, PrioritetDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PrioritetDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<PrioritetDodajResponse> Obradi([FromBody]PrioritetDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Prioritet
            {
                 Naziv=request.Naziv,
                 Opis=request.Opis
            };
            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new PrioritetDodajResponse
            {
                 PrioritetID=noviObj.PrioritetID
            };
        }
    }
}
