using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Posloprimaoc.Dodaj
{
    [Route("posloprimaoc-dodaj")]
    public class PosloprimaocDodajEndpoint : MyBaseEndpoint<PosloprimaocDodajRequest, PosloprimaocDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosloprimaocDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PosloprimaocDodajResponse> Obradi([FromBody] PosloprimaocDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Posloprimaoc
            {
                KorisnikId = request.KorisnikId,
                Strucnost = request.Strucnost

            };
            _applicationDbContext.Posloprimaoci.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new PosloprimaocDodajResponse
            {
                Posloprimaocid = noviObj.PosloprimaocID
            };
        }
    }
}