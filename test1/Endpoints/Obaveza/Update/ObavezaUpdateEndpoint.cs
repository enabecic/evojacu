using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Obaveza.Update
{
    [Route("Obaveza-update")]
    public class ObavezaUpdateEndpoint : MyBaseEndpoint<ObavezaUpdateRequest, ObavezaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ObavezaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<ObavezaUpdateResponse> Obradi([FromBody]ObavezaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var obaveza=_applicationDbContext.Obaveze.FirstOrDefault(x=>x.ObavezaID==request.ObavezaID);    

            if (obaveza==null)
            {
                throw new Exception("Ne postoji obaveza sa ID = " + request.ObavezaID);
            }

            obaveza.DatumRokaIzvrsenja = request.DatumRokaIzvrsenja;
            obaveza.Opis=request.Opis;

            await _applicationDbContext.SaveChangesAsync();

            return new ObavezaUpdateResponse
            {
                ObavezaID = obaveza.ObavezaID
            };
        }
    }
}
