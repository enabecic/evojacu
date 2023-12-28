using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.StanjePlacanja.Dodaj
{


    [Route("StanjePlacanja-dodaj")]
    public class StanjePlacanjaDodajEndpoint : MyBaseEndpoint<StanjePlacanjaDodajRequest, StanjePlacanjaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public StanjePlacanjaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<StanjePlacanjaDodajResponse> Obradi([FromBody] StanjePlacanjaDodajRequest request, CancellationToken cancellationToken = default)

        {
            var noviObj = new evojacu.Models.StanjePlacanja
            {

                OpisStanja = request.OpisStanja
            };
            _applicationDbContext.StanjaPlacanja.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new StanjePlacanjaDodajResponse
            {
               StanjePlacanjaID = noviObj.StanjePlacanjaID
            };
        }
    }

}

