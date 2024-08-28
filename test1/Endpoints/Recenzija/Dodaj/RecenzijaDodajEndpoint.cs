using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Recenzija.Dodaj
{
    [Route("Recenzija-dodaj")]
    public class RecenzijaDodajEndpoint : MyBaseEndpoint<RecenzijaDodajRequest, RecenzijaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RecenzijaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<RecenzijaDodajResponse> Obradi([FromBody]RecenzijaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Recenzija
            {
                Komentar = request.Komentar,
                PosaoID = request.PosaoID,            
                PosloprimaocID = request.PosloprimaocID,
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new RecenzijaDodajResponse
            {
                RecenzijaID = noviObj.RecenzijaID
            };
        }
    }
}
