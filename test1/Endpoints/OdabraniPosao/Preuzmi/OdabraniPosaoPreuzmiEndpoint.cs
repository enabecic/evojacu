using evojacu.Endpoints.Grad.Preuzmi;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.OdabraniPosao.Preuzmi
{
    [Route("OdabraniPosao-preuzmi")]
    public class OdabraniPosaoPreuzmiEndpoint : MyBaseEndpoint<OdabraniPosaoPreuzmiRequest, OdabraniPosaoPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public OdabraniPosaoPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public override async Task<OdabraniPosaoPreuzmiResponse> Obradi([FromQuery] OdabraniPosaoPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var odabraniPoslovi = await _applicationDbContext.OdabraniPoslovi
                .Select(x => new OdabraniPosaoPreuzmiResponseOdabraniPoslovi()
                {
                    PosaoID= x.PosaoID,
                    PosloprimaocID= x.PosloprimaocID,
                    DatumOdabira= x.DatumOdabira,
                    OdabraniPosaoID= x.PosaoID,
                    Adresa=x.Posao.Adresa,
                    Cijena=x.Posao.Cijena,
                    Username=x.Posloprimaoc.Korisnik.Username,
                    DatumObjave = x.Posao.DatumObjave,
                    OpisPosla = x.Posao.OpisPosla,
                    GPS=x.Posao.UkljucenGPS,
                    Grad=x.Posao.Grad.Naziv,
                    RokIzvrsavanja=x.Posao.VrijemeIzvrsavanja.KrajVremena,
                    NazivPosla=x.Posao.Zadatak.Naziv
                }).ToListAsync(cancellationToken: cancellationToken);

            return new OdabraniPosaoPreuzmiResponse
            {
                odabraniPoslovi = odabraniPoslovi
            };

            


        }
    }
}
