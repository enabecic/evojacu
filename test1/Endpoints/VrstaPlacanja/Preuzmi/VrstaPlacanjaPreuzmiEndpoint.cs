using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.VrstaPlacanja.Preuzmi
{
    [Route("VrstaPlacanja-preuzmi")]
    public class VrstaPlacanjaPreuzmiEndpoint : MyBaseEndpoint<VrstaPlacanjaPreuzmiRequest, VrstaPlacanjaPreuzmiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public VrstaPlacanjaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public override async Task<VrstaPlacanjaPreuzmiResponse> Obradi([FromQuery] VrstaPlacanjaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var vrste = await _applicationDbContext.VrstePlacanja.Where(x => request.TipPlacanja == null || x.TipPlacanja.ToLower().StartsWith(request.TipPlacanja.ToLower()))
                .Select(x => new VrstaPlacanjaPreuzmiResponseVrsta()
                {
                    VrstaPlacanjaID = x.VrstaPlacanjaID,
                    TipPlacanja = x.TipPlacanja,
                    BrojKartice = x.BrojKartice,
                    DatumIsteka = x.DatumIsteka
                }).ToListAsync(cancellationToken: cancellationToken);

            return new VrstaPlacanjaPreuzmiResponse
            {
                Vrste = vrste
            };
        }



    }
}
