using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;


namespace evojacu.Endpoints.VrijemeIzvrsavanja.Update
{
    [Route("VrijemeIzvrsavanja-update")]

    public class VrijemeIzvrsavanjaUpdateEndpoint : MyBaseEndpoint<VrijemeIzvrsavanjaUpdateRequest, VrijemeIzvrsavanjaUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public VrijemeIzvrsavanjaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<VrijemeIzvrsavanjaUpdateResponse> Obradi([FromQuery] VrijemeIzvrsavanjaUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var vremena = _applicationDbContext.VremenaIzvrsavanja.FirstOrDefault(x => x.VrijemeIzvrsavanjaID == request.vrijemeid);

            if (vremena == null)
            {
                throw new Exception("Nema vremena koji posjeduje ID -> " + request.vrijemeid);
            }

            vremena.PocetakVremena = request.PocetakVr;
            vremena.KrajVremena = request.KrajVr;


            await _applicationDbContext.SaveChangesAsync();

            return new VrijemeIzvrsavanjaUpdateResponse
            {
                VrijemeID = vremena.VrijemeIzvrsavanjaID
            };
        }
    }
}
