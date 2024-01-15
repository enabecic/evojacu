using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Recenzija.Preuzmi
{
    [Route("Recenzija-preuzmi")]
    public class RecenzijaPreuzmiEndpoint : MyBaseEndpoint<RecenzijaPreuzmiRequest, RecenzijaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public RecenzijaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public override async Task<RecenzijaPreuzmiResponse> Obradi([FromQuery]RecenzijaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var recenzije = await _applicationDbContext.Recenzije.Where(x => request.Ocjena == null || x.Ocjena == request.Ocjena)
               .Select(x => new RecenzijaPreuzmiResponseRecenzija()
               {
                   RecenzijaID = x.RecenzijaID,
                   PosaoID = x.PosaoID,
                   OpisPosla=x.Posao.OpisPosla,
                   PoslodavacID = x.PoslodavacID,
                   UserPoslodavac=x.Poslodavac.Korisnik.Username,
                   PosloprimaocID = x.PosloprimaocID,
                   UserPosloprimaoc=x.Posloprimaoc.Korisnik.Username,
                   Komentar = x.Komentar,
                   Ocjena = x.Ocjena

               }).ToListAsync(cancellationToken: cancellationToken);


            return new RecenzijaPreuzmiResponse
            {
                Recenzije = recenzije
            };
        }
    }
}
