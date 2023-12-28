using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.VrstaPlacanja.Dodaj
{
    [Route("VrstaPlacanja-dodaj")]
    public class VrstaPlacanjaDodajEndpoint : MyBaseEndpoint<VrstaPlacanjaDodajRequest, VrstaPlacanjaDodajResponse>
    {


        private readonly evojacuDBContext _applicationDbContext;

        public VrstaPlacanjaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<VrstaPlacanjaDodajResponse> Obradi([FromBody] VrstaPlacanjaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.VrstaPlacanja
            {
                TipPlacanja = request.TipPlacanja,
                BrojKartice = request.BrojKartice,
                DatumIsteka = request.DatumIsteka
            };

            _applicationDbContext.VrstePlacanja.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new VrstaPlacanjaDodajResponse
            {
                VrstaPlacanjaId = noviObj.VrstaPlacanjaID
            };

        }





    }
}
