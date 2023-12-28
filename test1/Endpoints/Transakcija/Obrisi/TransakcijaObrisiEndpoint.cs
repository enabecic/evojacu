using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Transakcija.Obrisi
{
    [Route("Transakcija-obrisi")]
    public class TransakcijaObrisiEndpoint : MyBaseEndpoint<TransakcijaObrisiRequest, TransakcijaObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public TransakcijaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<TransakcijaObrisiResponse> Obradi([FromQuery]TransakcijaObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var transakcija=_applicationDbContext.Transakcije.FirstOrDefault(x=>x.TransakcijaID==request.TransakcijaID);

            if (transakcija==null)
            {
                throw new Exception("Ne postoji transakcija sa ID = " + request.TransakcijaID);
            }

            _applicationDbContext.Remove(transakcija);
            await _applicationDbContext.SaveChangesAsync();

            return new TransakcijaObrisiResponse
            {

            };
        }
    }
}
