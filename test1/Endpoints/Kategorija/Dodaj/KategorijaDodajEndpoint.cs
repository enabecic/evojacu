using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
//using System.Reflection.Metadata.Ecma335;

namespace evojacu.Endpoints.Kategorija.Dodaj
{
    [Route("Kategorija-dodaj")]
    public class KategorijaDodajEndpoint : MyBaseEndpoint<KategorijaDodajRequest, KategorijaDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KategorijaDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }


        [HttpPost]
        public override async Task<KategorijaDodajResponse> Obradi([FromBody]KategorijaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Kategorija
            {
                 Naziv=request.Naziv
            };
            _applicationDbContext.Kategorije.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new KategorijaDodajResponse
            {
                 KategorijaID=noviObj.KategorijaID
            };
        }
    }
}
