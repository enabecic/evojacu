using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Recenzija.Update
{
    [Route("Recenzija-update")]
    public class RecenzijaUpdateEndpoint : MyBaseEndpoint<RecenzijaUpdateRequest, RecenzijaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RecenzijaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<RecenzijaUpdateResponse> Obradi([FromBody]RecenzijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var recenzija = _applicationDbContext.Recenzije.FirstOrDefault(x => x.RecenzijaID == request.RecenzijaID);

            if (recenzija == null)
            {
                throw new Exception("Ne postoji recenzija sa ID = " + request.RecenzijaID);
            }

            
            recenzija.Komentar = request.Komentar;

            await _applicationDbContext.SaveChangesAsync();


            return new RecenzijaUpdateResponse
            {
                RecenzijaID = recenzija.RecenzijaID
            };
        }
    }
}
