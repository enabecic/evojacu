using evojacu.Endpoints.Grad.Obrisi;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.OdabraniPosao.Obrisi
{
    [Route("OdabraniPosao-obrisi")]
    public class OdabraniPosaoObrisiEndpoint : MyBaseEndpoint<OdabraniPosaoObrisiRequest, OdabraniPosaoObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public OdabraniPosaoObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpDelete]
        public override async Task<OdabraniPosaoObrisiResponse> Obradi([FromQuery] OdabraniPosaoObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var odabraniPoslovi = _applicationDbContext.OdabraniPoslovi.FirstOrDefault(x => x.OdabraniPosaoID == request.OdabraniPosaoID);

            if (odabraniPoslovi == null)
            {
                throw new Exception("Nema odabranih poslova koji posjeduje ID -> " + request.OdabraniPosaoID);
            }

            _applicationDbContext.Remove(odabraniPoslovi);
            await _applicationDbContext.SaveChangesAsync();

            return new OdabraniPosaoObrisiResponse
            {

            };
        }
    }
}
