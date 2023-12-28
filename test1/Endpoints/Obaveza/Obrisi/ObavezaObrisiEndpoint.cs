using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Obaveza.Obrisi
{

    [Route("Obaveza-obrisi")]
    public class ObavezaObrisiEndpoint : MyBaseEndpoint<ObavezaObrisiRequest, ObavezaObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ObavezaObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpDelete]
        public override async Task<ObavezaObrisiResponse> Obradi([FromQuery]ObavezaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var obaveza=_applicationDbContext.Obaveze.FirstOrDefault(x=>x.ObavezaID== request.ObavezaID);

            if (obaveza==null)
            {
                throw new Exception("Ne postoji obaveza sa ID = " + request.ObavezaID);
            }

            _applicationDbContext.Remove(obaveza);
            await _applicationDbContext.SaveChangesAsync();

            return new ObavezaObrisiResponse
            {

            };
        }
    }
}
