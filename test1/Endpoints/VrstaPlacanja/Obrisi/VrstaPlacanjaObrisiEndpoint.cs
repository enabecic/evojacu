using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.VrstaPlacanja.Obrisi
{
    [Route("VrstaPlacanja-obrisi")]
    public class VrstaPlacanjaObrisiEndpoint : MyBaseEndpoint<VrstaPlacanjaObrisiRequest, VrstaPlacanjaObrisiResponse>
    {


        private readonly evojacuDBContext _applicationDbContext;

        public VrstaPlacanjaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<VrstaPlacanjaObrisiResponse> Obradi([FromQuery] VrstaPlacanjaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var vrstePlacanja = _applicationDbContext.VrstePlacanja.FirstOrDefault(x => x.VrstaPlacanjaID == request.VrstaPlacanjaID);

            if (vrstePlacanja == null)
            {
                throw new Exception("Ne postoji vrsta placanja sa ID= " + request.VrstaPlacanjaID);
            }

            _applicationDbContext.Remove(vrstePlacanja);
            await _applicationDbContext.SaveChangesAsync();

            return new VrstaPlacanjaObrisiResponse
            {

            };
        }



    }
}
