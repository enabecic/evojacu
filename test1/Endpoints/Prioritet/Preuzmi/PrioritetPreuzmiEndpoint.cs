using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Prioritet.Preuzmi
{
    [Route("Prioritet-preuzmi")]
    public class PrioritetPreuzmiEndpoint : MyBaseEndpoint<PrioritetPreuzmiRequest, PrioritetPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PrioritetPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }


        [HttpGet]
        public override async Task<PrioritetPreuzmiResponse> Obradi([FromQuery]PrioritetPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var prioriteti = await _applicationDbContext.Prioriteti.Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())).
                Select(x => new PrioritetPreuzmiResponsePrioritet()
                {
                     PrioritetID=x.PrioritetID,
                     Naziv=x.Naziv,
                     Opis=x.Opis
                }).ToListAsync(cancellationToken: cancellationToken);

            return new PrioritetPreuzmiResponse
            {
                Prioriteti = prioriteti
            };
        }




    }
}
