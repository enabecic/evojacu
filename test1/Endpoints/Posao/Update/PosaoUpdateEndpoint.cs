using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            posao.Cijena=request.Cijena;
            posao.UkljucenGPS = request.UkljucenGPS;

            await _applicationDbContext.SaveChangesAsync();


            return new PosaoUpdateResponse
            {
                PosaoID = posao.ZadatakID
            };
        }


        [HttpPost("odaberi/{posaoID}")]
        public async Task<IActionResult> OdaberiPosao(int posaoID, CancellationToken cancellationToken = default)
        {
            var posao = await _applicationDbContext.Poslovi.FirstOrDefaultAsync(x => x.ZadatakID == posaoID, cancellationToken);

            if (posao == null)
            {
                return NotFound(new { Message = "Posao not found with ID = " + posaoID });
            }

            posao.jeOdabran = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new { PosaoID = posao.ZadatakID });
        }



    }
}
