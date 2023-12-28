using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.StanjePlacanja.Obrisi
{


    [Route("StanjePlacanja-obrisi")]

    public class StanjePlacanjaObrisiEndpoint : MyBaseEndpoint<StanjePlacanjaObrisiRequest, StanjePlacanjaObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public StanjePlacanjaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<StanjePlacanjaObrisiResponse> Obradi([FromQuery] StanjePlacanjaObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var stanjeplacanja = _applicationDbContext.StanjaPlacanja.FirstOrDefault(x => x.StanjePlacanjaID == request.StanjePlacanjaID);

            if (stanjeplacanja == null)
            {
                throw new Exception("Nema stanja placanja koji posjeduje ID -> " + request.StanjePlacanjaID);
            }

            _applicationDbContext.Remove(stanjeplacanja);
            await _applicationDbContext.SaveChangesAsync();

            return new StanjePlacanjaObrisiResponse
            {

            };
        }
    }

}
