using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Zadatak.Dodaj
{
    [Route("Zadatak-dodaj")]
    public class ZadatakDodajEndpoint : MyBaseEndpoint<ZadatakDodajRequest, ZadatakDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<ZadatakDodajResponse> Obradi([FromBody]ZadatakDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Zadatak
            {
                Opis = request.Opis,
                Naziv=request.Naziv,
                KategorijaID=request.KategorijaID
                
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new ZadatakDodajResponse
            {
                ZadatakId = noviObj.ZadatakId
            };
        }
    }
}
