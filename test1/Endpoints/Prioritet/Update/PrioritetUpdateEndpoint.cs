using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Prioritet.Update
{
    [Route("Prioritet-update")]
    public class PrioritetUpdateEndpoint : MyBaseEndpoint<PrioritetUpdateRequest, PrioritetUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PrioritetUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        [HttpPost]
        public override async Task<PrioritetUpdateResponse> Obradi([FromBody]PrioritetUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var prioritet=_applicationDbContext.Prioriteti.FirstOrDefault(x=>x.PrioritetID==request.PrioritetID);  
            
            if (prioritet==null)
            {
                throw new Exception("Ne postoji prioritet sa ID= " + request.PrioritetID);
            }

            prioritet.Naziv=request.Naziv;
            prioritet.Opis=request.Opis;

           await _applicationDbContext.SaveChangesAsync();

            return new PrioritetUpdateResponse
            {
                 PrioritetID= prioritet.PrioritetID
            };

        }
    }
}
