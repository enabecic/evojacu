using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Posao.Update
{
    [Route("Posao-update")]
    public class PosaoUpdateEndpoint : MyBaseEndpoint<PosaoUpdateRequest, PosaoUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosaoUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<PosaoUpdateResponse> Obradi([FromBody]PosaoUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var posao = _applicationDbContext.Poslovi.FirstOrDefault(x => x.ZadatakID == request.PosaoID);

            if (posao == null)
            {
                throw new Exception("Ne postoji posao sa ID = " + request.PosaoID);
            }

            posao.Adresa = request.Adresa;
            posao.FazaPoslaId = request.FazaPoslaID;
            posao.VrijemeIzvrsavanjaId = request.VrijemeIzvrsavanjaID;
            posao.OpisPosla=request.OpisPosla;


            await _applicationDbContext.SaveChangesAsync();


            return new PosaoUpdateResponse
            {
                PosaoID = posao.ZadatakID
            };
        }
    }
}
