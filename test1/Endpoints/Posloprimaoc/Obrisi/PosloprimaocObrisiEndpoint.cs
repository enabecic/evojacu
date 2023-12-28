using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Posloprimaoc.Obrisi
{
    [Route("posloprimaoc-obrisi")]

    public class PosloprimaocObrisiEndpoint : MyBaseEndpoint<PosloprimaocObrisiRequest, PosloprimaocObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public PosloprimaocObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<PosloprimaocObrisiResponse> Obradi([FromQuery] PosloprimaocObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var posloprimaoci = _applicationDbContext.Posloprimaoci.FirstOrDefault(x => x.PosloprimaocID == request.PosloprimaocID);

            if (posloprimaoci == null)
            {
                throw new Exception("Nema posloprimaoca koji posjeduje ID -> " + request.PosloprimaocID);
            }

            _applicationDbContext.Remove(posloprimaoci);
            await _applicationDbContext.SaveChangesAsync();

            return new PosloprimaocObrisiResponse
            {

            };
        }
    }

}

