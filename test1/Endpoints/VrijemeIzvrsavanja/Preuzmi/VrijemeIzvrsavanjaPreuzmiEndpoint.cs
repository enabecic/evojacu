using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace evojacu.Endpoints.VrijemeIzvrsavanja.Preuzmi
{
    [Route("VrijemeIzvrsavanja-preuzmi")]

    public class VrijemeIzvrsavanjaPreuzmiEndpoint : MyBaseEndpoint<VrijemeIzvrsavanjaPreuzmiRequest, VrijemeIzvrsavanjaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public VrijemeIzvrsavanjaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<VrijemeIzvrsavanjaPreuzmiResponse> Obradi([FromQuery] VrijemeIzvrsavanjaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var vremenaIzvrsavanja = await _applicationDbContext.VremenaIzvrsavanja
                .Select(x => new VrijemeIzvrsavanjaPreuzmiResponseVremenaIzvrsavanja()
                {
                    VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaID,
                    KrajVremena = x.KrajVremena.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH"))
                }).ToListAsync(cancellationToken: cancellationToken);

            return new VrijemeIzvrsavanjaPreuzmiResponse
            {
                VremenaIzvrsavanja = vremenaIzvrsavanja
            }; 

          


        }


    }
}
