using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Gost.Preuzmi
{
    [Route("Gost-preuzmi")]
    public class GostPreuzmiEndpoint : MyBaseEndpoint<GostPreuzmiRequest, GostPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public GostPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<GostPreuzmiResponse> Obradi([FromQuery]GostPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var gosti = await _applicationDbContext.Gosti.Where(x => request.BrojPosjeta == null || x.BrojPosjeta == request.BrojPosjeta)
                .Select(x => new GostPreuzmiResponseGost()
                {
                    GostID = x.GostID,
                    BrojPosjeta = x.BrojPosjeta
                }).ToListAsync(cancellationToken: cancellationToken);

            return new GostPreuzmiResponse
            {
                Gosti = gosti
            };
        }
    }
}
