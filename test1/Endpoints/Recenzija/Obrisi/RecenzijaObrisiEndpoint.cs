using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Recenzija.Obrisi
{
    [Route("Recenzija-obrisi")]
    public class RecenzijaObrisiEndpoint : MyBaseEndpoint<RecenzijaObrisiRequest, RecenzijaObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RecenzijaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<RecenzijaObrisiResponse> Obradi([FromQuery]RecenzijaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var recenzija = _applicationDbContext.Recenzije.FirstOrDefault(x => x.RecenzijaID == request.RecenzijaID);

            if (recenzija == null)
            {
                throw new Exception("Ne postoji recenzija sa ID = " + request.RecenzijaID);
            }

            _applicationDbContext.Remove(recenzija);
            await _applicationDbContext.SaveChangesAsync();

            return new RecenzijaObrisiResponse
            {

            };
        }
    }
}
