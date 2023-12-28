using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Transakcija.Dodaj
{
    [Route("Transakcija-dodaj")]
    public class TransakcijaDodajEndpoint : MyBaseEndpoint<TransakcijaDodajRequest, TransakcijaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public TransakcijaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<TransakcijaDodajResponse> Obradi([FromBody]TransakcijaDodajRequest request, CancellationToken cancellationToken = default)
        {

            var noviObj = new evojacu.Models.Transakcija
            {
                 Iznos=request.Iznos,
                 VrijemeTransakcije=request.VrijemeTransakcije,
                 NacinPlacanjaId=request.NacinPlacanjaId,
                 StanjePlacanjaId=request.StanjePlacanjaId,
                 Opis=request.Opis,
                 PosaoID=request.PosaoID,
                 PoslodavacID=request.PoslodavacID
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new TransakcijaDodajResponse
            {
                 TransakcijaID=noviObj.TransakcijaID
            };
        }
    }
}
