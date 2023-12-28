using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.FazaPosla.Obrisi
{
    [Route("FazaPosla-obrisi")]

    public class FazaPoslaObrisiEndpoint : MyBaseEndpoint<FazaPoslaObrisiRequest, FazaPoslaObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public FazaPoslaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<FazaPoslaObrisiResponse> Obradi([FromQuery] FazaPoslaObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var fazeposlova = _applicationDbContext.FazePoslova.FirstOrDefault(x => x.FazaPoslaID == request.FazaPoslaId);

            if (fazeposlova == null)
            {
                throw new Exception("Nema faze posla koji posjeduje ID -> " + request.FazaPoslaId);
            }

            _applicationDbContext.Remove(fazeposlova);
            await _applicationDbContext.SaveChangesAsync();

            return new FazaPoslaObrisiResponse
            {

            };
        }
    }

}
