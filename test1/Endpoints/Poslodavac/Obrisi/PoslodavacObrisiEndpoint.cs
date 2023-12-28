using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Poslodavac.Obrisi
{
    [Route("poslodavac-obrisi")]

    public class PoslodavacObrisiEndpoint : MyBaseEndpoint<PoslodavacObrisiRequest, PoslodavacObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public PoslodavacObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<PoslodavacObrisiResponse> Obradi([FromQuery] PoslodavacObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var poslodavci = _applicationDbContext.Poslodavci.FirstOrDefault(x => x.PoslodavacID == request.PoslodavacID);

            if (poslodavci == null)
            {
                throw new Exception("Nema poslodavca koji posjeduje ID -> " + request.PoslodavacID);
            }

            _applicationDbContext.Remove(poslodavci);
            await _applicationDbContext.SaveChangesAsync();

            return new PoslodavacObrisiResponse
            {

            };
        }
    }

}

