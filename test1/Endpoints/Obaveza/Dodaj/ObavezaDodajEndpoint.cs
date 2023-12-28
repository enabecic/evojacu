using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Obaveza.Dodaj
{
    [Route("Obaveza-dodaj")]
    public class ObavezaDodajEndpoint : MyBaseEndpoint<ObavezaDodajRequest, ObavezaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ObavezaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<ObavezaDodajResponse> Obradi([FromBody]ObavezaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Obaveza
            {
                PosloprimaocID = request.PosloprimaocID,
                Opis=request.Opis,
                DatumRokaIzvrsenja=request.DatumRokaIzvrsenja,
                PrioritetID = request.PrioritetID,
            };


            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();


            return new ObavezaDodajResponse
            {
                 ObavezaID=noviObj.ObavezaID
            };
        }
    }
}
