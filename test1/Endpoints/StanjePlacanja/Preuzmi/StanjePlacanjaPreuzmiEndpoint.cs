using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.StanjePlacanja.Preuzmi
{
    [Route("StanjePlacannja-preuzmi")]

    public class StanjePlacanjaPreuzmiEndpoint : MyBaseEndpoint<StanjePlacanjaPreuzmiRequest, StanjePlacanjaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public StanjePlacanjaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<StanjePlacanjaPreuzmiResponse> Obradi([FromQuery] StanjePlacanjaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var stanjelista = _applicationDbContext.StanjaPlacanja.ToList();

            var stanjeplacanja = stanjelista.FirstOrDefault(k => k.OpisStanja == request.OpisStanja);

            if (stanjelista != null)
            {
                Console.WriteLine("Stanje je pronađeno!");
                throw new Exception("Postojece stanje");


            }
            else
            {
                Console.WriteLine("Stanje nije pronađeno!");
                throw new Exception("Ne postoji stanje");
            }


        }


    }
}
