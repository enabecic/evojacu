using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace test1.Endpoints.Poslodavaoc.Dodaj
{ 
    [Route("poslodavac-dodaj")]
    public class PoslodavacDodajEndpoint : MyBaseEndpoint<PoslodavacDodajRequest, PoslodavacDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PoslodavacDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PoslodavacDodajResponse> Obradi([FromBody] PoslodavacDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Poslodavac
            {
                KorisnikId = request.KorisnikId,
                NazivKompanije = request.NazivKompanije
               
            };
            _applicationDbContext.Poslodavci.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new PoslodavacDodajResponse
            {
                Poslodavacid = noviObj.PoslodavacID
            };
        }
    }
}