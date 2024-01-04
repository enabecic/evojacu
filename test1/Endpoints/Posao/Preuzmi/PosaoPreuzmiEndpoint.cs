using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Posao.Preuzmi
{
    [Route("Posao-preuzmi")]
    public class PosaoPreuzmiEndpoint : MyBaseEndpoint<PosaoPreuzmiRequest, PosaoPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PosaoPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public override async Task<PosaoPreuzmiResponse> Obradi([FromQuery]PosaoPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var poslovi = await _applicationDbContext.Poslovi.Where(x => request.OpisPosla == null || x.OpisPosla.ToLower().Contains(request.OpisPosla.ToLower()))
               .Select(x => new PosaoPreuzmiResponsePosao()
               {
                   FazaPoslaID = x.FazaPoslaId,
                   NazivFazePosla = x.FazaPosla.Naziv,
                   Adresa = x.Adresa,
                   JePonuda = x.JePonuda,
                   GradID = x.GradId,
                   VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                   NazivGrada = x.Grad.Naziv,
                   OpisPosla = x.OpisPosla,
                   KorisnikID = x.KorisnikID,
                   UserName = x.Korisnik.Username,
                   PosaoID = x.ZadatakID,
                   ZadatakStraniID= x.ZadatakStraniID,
                   NazivZadatka=x.Zadatak.Naziv
               }).ToListAsync(cancellationToken: cancellationToken);


            return new PosaoPreuzmiResponse
            {
                Poslovi = poslovi
            };
        }
    }
}
