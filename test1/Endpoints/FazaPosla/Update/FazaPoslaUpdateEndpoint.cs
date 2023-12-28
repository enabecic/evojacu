using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.FazaPosla.Update
{
    [Route("FazaPosla-update")]

    public class FazaPoslaUpdateEndpoint : MyBaseEndpoint<FazaPoslaUpdateRequest, FazaPoslaUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public FazaPoslaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<FazaPoslaUpdateResponse> Obradi([FromQuery] FazaPoslaUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var fazeposlova = _applicationDbContext.FazePoslova.FirstOrDefault(x => x.FazaPoslaID == request.Id);

            if (fazeposlova == null)
            {
                throw new Exception("Nema faze posla koji posjeduje ID -> " + request.Id);
            }

            fazeposlova.Naziv = request.Naziv;
            fazeposlova.Opis = request.Opis;


            await _applicationDbContext.SaveChangesAsync();

            return new FazaPoslaUpdateResponse
            {
                FazaPoslaId = fazeposlova.FazaPoslaID
            };
        }
    }
}
