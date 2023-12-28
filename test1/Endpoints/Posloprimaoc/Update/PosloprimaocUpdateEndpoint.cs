using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Posloprimaoc.Update
{
    [Route("posloprimaoci-update")]

    public class PosloprimaocUpdateEndpoint : MyBaseEndpoint<PosloprimaocUpdateRequest, PosloprimaocUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public PosloprimaocUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PosloprimaocUpdateResponse> Obradi([FromQuery] PosloprimaocUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var posloprimaoci = _applicationDbContext.Posloprimaoci.FirstOrDefault(x => x.PosloprimaocID == request.PosloprimaocID);

            if (posloprimaoci == null)
            {
                throw new Exception("Nema posloprimaoca koji posjeduje ID -> " + request.PosloprimaocID);
            }

            posloprimaoci.Strucnost = request.Strucnost;


            await _applicationDbContext.SaveChangesAsync();

            return new PosloprimaocUpdateResponse
            {
                Posloprimaocid = posloprimaoci.PosloprimaocID
            };
        }
    }

}


