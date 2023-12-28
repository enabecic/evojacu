using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Poslodavac.Update
{
    [Route("poslodavci-update")]

    public class PoslodavacUpdateEndpoint : MyBaseEndpoint<PoslodavacUpdateRequest, PoslodavacUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public PoslodavacUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PoslodavacUpdateResponse> Obradi([FromQuery] PoslodavacUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var poslodavci = _applicationDbContext.Poslodavci.FirstOrDefault(x => x.PoslodavacID == request.PoslodavacID);

            if (poslodavci == null)
            {
                throw new Exception("Nema poslodavca koji posjeduje ID -> " + request.PoslodavacID);
            }

            poslodavci.NazivKompanije = request.NazivKompanije;
          

            await _applicationDbContext.SaveChangesAsync();//izvrašva se "insert into Ispit value ...."

            return new PoslodavacUpdateResponse
            {
                Poslodavacid = poslodavci.PoslodavacID
            };
        }
    }

}


