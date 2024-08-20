using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using System.Globalization;

namespace evojacu.Endpoints.VrijemeIzvrsavanja.Dodaj
{


    [Route("VrijemeIzvrsavanja-dodaj")]
    public class VrijemeIzvrsavanjaDodajEndpoint : MyBaseEndpoint<VrijemeIzvrsavanjaDodajRequest, VrijemeIzvrsavanjaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public VrijemeIzvrsavanjaDodajEndpoint(evojacuDBContext applicationDbContext)

        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<VrijemeIzvrsavanjaDodajResponse> Obradi([FromBody] VrijemeIzvrsavanjaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.VrijemeIzvrsavanja
            {

                //PocetakVremena = request.PocetnoVr,
                KrajVremena= request.KrajnjeVr
            };
            _applicationDbContext.VremenaIzvrsavanja.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            var formatiranDatum = noviObj.KrajVremena.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH"));

            return new VrijemeIzvrsavanjaDodajResponse
            {
                VrijemeID = noviObj.VrijemeIzvrsavanjaID,
                KrajVremena = formatiranDatum
            };
        }
    }

}
