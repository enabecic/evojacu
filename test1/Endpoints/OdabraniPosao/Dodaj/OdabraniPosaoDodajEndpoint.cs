using evojacu.Endpoints.Grad.Dodaj;
using evojacu.Endpoints.Zadatak.Dodaj;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.OdabraniPosao.Dodaj
{
    [Route("OdabraniPosao-dodaj")]
    public class OdabraniPosaoDodajEndpoint : MyBaseEndpoint<OdabraniPosaoDodajRequest, OdabraniPosaoDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public OdabraniPosaoDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<OdabraniPosaoDodajResponse> Obradi([FromBody] OdabraniPosaoDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.OdabraniPosao
            {

                DatumOdabira = DateTime.Now,
                PosaoID= request.PosaoID,
                PosloprimaocID= request.PosloprimaocID
            };
            _applicationDbContext.OdabraniPoslovi.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new OdabraniPosaoDodajResponse
            {
                OdabraniPosaoID = noviObj.OdabraniPosaoID
            };
        }
    }
}
