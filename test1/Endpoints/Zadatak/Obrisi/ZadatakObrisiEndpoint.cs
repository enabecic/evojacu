using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Zadatak.Obrisi
{
    [Route("Zadatak-obrisi")]
    public class ZadatakObrisiEndpoint : MyBaseEndpoint<ZadatakObrisiRequest, ZadatakObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ZadatakObrisiResponse> Obradi([FromQuery]ZadatakObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var zadatak = _applicationDbContext.Zadaci.FirstOrDefault(x => x.ZadatakId == request.ZadatakId);

            if (zadatak == null)
            {
                throw new Exception("Ne postoji zadatak sa ID = " + request.ZadatakId);
            }

            _applicationDbContext.Remove(zadatak);
            await _applicationDbContext.SaveChangesAsync();

            return new ZadatakObrisiResponse
            {

            };
        }
    }
}
