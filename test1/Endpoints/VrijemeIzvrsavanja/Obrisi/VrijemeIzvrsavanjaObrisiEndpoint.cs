using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.VrijemeIzvrsavanja.Obrisi
{


    [Route("VrijemeIzvrsavanja-obrisi")]

    public class VrijemeIzvrsavanjaObrisiEndpoint : MyBaseEndpoint<VrijemeIzvrsavanjaObrisiRequest, VrijemeIzvrsavanjaObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public VrijemeIzvrsavanjaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<VrijemeIzvrsavanjaObrisiResponse> Obradi([FromQuery] VrijemeIzvrsavanjaObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var vrijeme = _applicationDbContext.VremenaIzvrsavanja.FirstOrDefault(x => x.VrijemeIzvrsavanjaID == request.VrijemeID);

            if (vrijeme == null)
            {
                throw new Exception("Nema vremena koji posjeduje ID -> " + request.VrijemeID);
            }

            _applicationDbContext.Remove(vrijeme);
            await _applicationDbContext.SaveChangesAsync();

            return new VrijemeIzvrsavanjaObrisiResponse
            {

            };
        }
    }

}
