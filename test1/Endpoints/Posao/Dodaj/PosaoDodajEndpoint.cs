using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Posao.Dodaj
{
    [Route("Posao-dodaj")]
    public class PosaoDodajEndpoint : MyBaseEndpoint<PosaoDodajRequest, PosaoDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosaoDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<PosaoDodajResponse> Obradi([FromBody]PosaoDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Posao
            {
                KorisnikID = request.KorisnikID,
                VrijemeIzvrsavanjaId = request.VrijemeIzvrsavanjaID,
                GradId = request.GradID,
                FazaPoslaId = request.FazaPoslaID,
                OpisPosla = request.OpisPosla,
                JePonuda = request.JePonuda,
                Adresa = request.Adresa,
                ZadatakStraniID=request.ZadatakStraniID
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

            return new PosaoDodajResponse
            {
                PosaoID = noviObj.ZadatakID
            };
        }
    }
}
