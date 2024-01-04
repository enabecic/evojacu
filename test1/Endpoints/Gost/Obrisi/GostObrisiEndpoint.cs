using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Gost.Obrisi
{
    [Route("Gost-obrisi")]
    public class GostObrisiEndpoint : MyBaseEndpoint<GostObrisiRequest, GostObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public GostObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<GostObrisiResponse> Obradi([FromQuery]GostObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var gost = _applicationDbContext.Gosti.FirstOrDefault(x => x.GostID == request.GostID);

            if (gost == null)
            {
                throw new Exception("Ne postoji gost sa ID= " + request.GostID);
            }

            _applicationDbContext.Remove(gost);
            await _applicationDbContext.SaveChangesAsync();

            return new GostObrisiResponse
            {

            };
        }
    }
}
