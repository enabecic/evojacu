using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Posao.Obrisi
{
    [Route("Posao-obrisi")]
    public class PosaoObrisiEndpoint : MyBaseEndpoint<PosaoObrisiRequest, PosaoObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosaoObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<PosaoObrisiResponse> Obradi([FromQuery]PosaoObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var posao = _applicationDbContext.Poslovi.FirstOrDefault(x => x.ZadatakID == request.PosaoID);

            if (posao == null)
            {
                throw new Exception("Ne postoji posao sa ID = " + request.PosaoID);
            }

            _applicationDbContext.Remove(posao);
            await _applicationDbContext.SaveChangesAsync();

            return new PosaoObrisiResponse
            {

            };
        }
    }
}
