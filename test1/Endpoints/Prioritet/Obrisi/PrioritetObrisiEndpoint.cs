using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Prioritet.Obrisi
{
    [Route("Prioritet-obrisi")]
    public class PrioritetObrisiEndpoint : MyBaseEndpoint<PrioritetObrisiRequest, PrioritetObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PrioritetObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<PrioritetObrisiResponse> Obradi([FromQuery]PrioritetObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var prioritet=_applicationDbContext.Prioriteti.FirstOrDefault(x=>x.PrioritetID== request.PrioritetID);
            if (prioritet==null) {
                throw new Exception("Ne postoji prioritet sa ID= " + request.PrioritetID);
            }

            _applicationDbContext.Remove(prioritet);
            await _applicationDbContext.SaveChangesAsync();

            return new PrioritetObrisiResponse
            {

            };
        }
    }
}
