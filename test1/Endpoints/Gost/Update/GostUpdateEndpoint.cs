using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Gost.Update
{
    [Route("Gost-update")]
    public class GostUpdateEndpoint : MyBaseEndpoint<GostUpdateRequest, GostUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public GostUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<GostUpdateResponse> Obradi([FromBody]GostUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var gosti = _applicationDbContext.Gosti.FirstOrDefault(x => x.GostID == request.GostID);

            if (gosti == null)
            {
                throw new Exception("Ne postoji gost sa ID= " + request.GostID);
            }

            gosti.BrojPosjeta = request.BrojPosjeta;

            await _applicationDbContext.SaveChangesAsync();

            return new GostUpdateResponse
            {
                GostID = gosti.GostID
            };
        }
    }
}
