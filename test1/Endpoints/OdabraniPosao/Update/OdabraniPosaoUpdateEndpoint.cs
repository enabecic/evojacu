using evojacu.Endpoints.Grad.Update;
using evojacu.Endpoints.Zadatak.Update;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.OdabraniPosao.Update
{
    [Route("OdabraniPosao-update")]
    public class OdabraniPosaoUpdateEndpoint : MyBaseEndpoint<OdabraniPosaoUpdateRequest, OdabraniPosaoUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public OdabraniPosaoUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<OdabraniPosaoUpdateResponse> Obradi([FromQuery] OdabraniPosaoUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var odabraniPoslovi = _applicationDbContext.OdabraniPoslovi.FirstOrDefault(x => x.OdabraniPosaoID == request.OdabraniPosaoID);

            if (odabraniPoslovi == null)
            {
                throw new Exception("Nema odagranog posla koji posjeduje ID -> " + request.OdabraniPosaoID);
            }

            odabraniPoslovi.PosaoID = request.PosaoID;
            odabraniPoslovi.PosloprimaocID = request.PosloprimaocID;



            await _applicationDbContext.SaveChangesAsync();

            return new OdabraniPosaoUpdateResponse
            {
                OdabraniPosaoID = odabraniPoslovi.OdabraniPosaoID
            };
        }

    }
}
