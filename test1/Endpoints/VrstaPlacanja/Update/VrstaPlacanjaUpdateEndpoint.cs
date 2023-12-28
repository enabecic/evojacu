using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.VrstaPlacanja.Update
{
    [Route("VrstaPlacanja-update")]
    public class VrstaPlacanjaUpdateEndpoint : MyBaseEndpoint<VrstaPlacanjaUpdateRequest, VrstaPlacanjaUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public VrstaPlacanjaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<VrstaPlacanjaUpdateResponse> Obradi([FromBody] VrstaPlacanjaUpdateRequest request, CancellationToken cancellationToken = default)
        {


            var vrste = _applicationDbContext.VrstePlacanja.FirstOrDefault(x => x.VrstaPlacanjaID == request.VrstaPlacanjaID);
            if (vrste == null)
            {
                throw new Exception("Nema vrste plaćanja sa ID= " + request.VrstaPlacanjaID);
            }

            vrste.TipPlacanja = request.TipPlacanja;
            vrste.BrojKartice = request.BrojKartice;
            vrste.DatumIsteka = request.DatumIsteka;

            await _applicationDbContext.SaveChangesAsync();

            return new VrstaPlacanjaUpdateResponse
            {
                Id = vrste.VrstaPlacanjaID

            };
        }


    }
}
