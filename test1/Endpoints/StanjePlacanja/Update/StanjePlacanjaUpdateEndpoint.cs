using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;


namespace evojacu.Endpoints.StanjePlacanja.Update
{
    [Route("StanjePlacanja-update")]

    public class StanjePlacanjaUpdateEndpoint : MyBaseEndpoint<StanjePlacanjaUpdateRequest, StanjePlacanjaUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public StanjePlacanjaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<StanjePlacanjaUpdateResponse> Obradi([FromQuery] StanjePlacanjaUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var stanja = _applicationDbContext.StanjaPlacanja.FirstOrDefault(x => x.StanjePlacanjaID == request.StanjePlacanjaID);

            if (stanja == null)
            {
                throw new Exception("Nema stanja koji posjeduje ID -> " + request.StanjePlacanjaID);
            }

            stanja.OpisStanja = request.Opis;


            await _applicationDbContext.SaveChangesAsync();

            return new StanjePlacanjaUpdateResponse
            {
                StanjeID = stanja.StanjePlacanjaID
            };
        }
    }
}
