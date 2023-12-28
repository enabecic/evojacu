using Microsoft.AspNetCore.Mvc;

using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Posloprimaoc.Preuzmi
{
    [Route("Posloprimaoc-preuzmi")]
    public class PosloprimaocPreuzmiEndpoint : MyBaseEndpoint<PosloprimaocPreuzmiRequest, PosloprimaocPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosloprimaocPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<PosloprimaocPreuzmiResponse> Obradi([FromQuery] PosloprimaocPreuzmiRequest request, CancellationToken cancellationToken = default)
        {


            var posloprimaoci = _applicationDbContext.Posloprimaoci.ToList();

            var posloprimaocilista = posloprimaoci.FirstOrDefault(o => o.PosloprimaocID == request.PosloprimaocID);

            if (posloprimaocilista == null)
            {

                throw new Exception("Ne postoji posloprimaoc sa tim ID-om");
            }

            return new PosloprimaocPreuzmiResponse
            {

                KorisnikId = posloprimaocilista.KorisnikId,
                Strucnost = posloprimaocilista.Strucnost
            };
        }

    }
}
