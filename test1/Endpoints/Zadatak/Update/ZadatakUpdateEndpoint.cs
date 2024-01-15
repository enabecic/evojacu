using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Zadatak.Update
{
    [Route("Zadatak-update")]
    public class ZadatakUpdateEndpoint : MyBaseEndpoint<ZadatakUpdateRequest, ZadatakUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<ZadatakUpdateResponse> Obradi([FromBody]ZadatakUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var zadatak = _applicationDbContext.Zadaci.FirstOrDefault(x => x.ZadatakId == request.ZadatakId);

            if (zadatak == null)
            {
                throw new Exception("Ne postoji zadatak sa ID = " + request.ZadatakId);
            }

            zadatak.Opis = request.Opis;
            zadatak.Naziv=request.Naziv;

            await _applicationDbContext.SaveChangesAsync();


            return new ZadatakUpdateResponse
            {
                ZadatakId = zadatak.ZadatakId
            };
        }
    }
}
