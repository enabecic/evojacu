using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Gost.Dodaj
{
    [Route("Gost-dodaj")]
    public class GostDodajEndpoint : MyBaseEndpoint<GostDodajRequest, GostDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public GostDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<GostDodajResponse> Obradi([FromBody]GostDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Gost
            {
                BrojPosjeta = request.BrojPosjeta
            };
            _applicationDbContext.Gosti.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new GostDodajResponse
            {
                GostID = noviObj.GostID
            };
        }
    }
}
