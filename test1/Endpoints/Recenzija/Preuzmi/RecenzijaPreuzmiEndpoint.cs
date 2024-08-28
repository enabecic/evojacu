using evojacu.Endpoints.Posao.Preuzmi;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            var recenzije = await _applicationDbContext.Recenzije
               .Select(x => new RecenzijaPreuzmiResponseRecenzija()
               {
                   RecenzijaID = x.RecenzijaID,
                   PosaoID = x.PosaoID,
                   PoslodavacID = x.Posao.PoslodavacID,
                   UserPoslodavac=x.Posao.Poslodavac.Korisnik.Username,
                   PosloprimaocID = x.PosloprimaocID,
                   UserPosloprimaoc=x.Posloprimaoc.Korisnik.Username,
                   Komentar = x.Komentar,
                  

               }).ToListAsync(cancellationToken: cancellationToken);


            return new RecenzijaPreuzmiResponse
            {
                Recenzije = recenzije
            };
        }


        [HttpGet("{id}")]
        public async Task<RecenzijaPreuzmiResponse> GetRecenzijaByPosaoId(int id, CancellationToken cancellationToken = default)
        {
           
            var recenzije = await _applicationDbContext.Recenzije
                .Where(x => x.PosaoID == id)
                .Select(x => new RecenzijaPreuzmiResponseRecenzija()
                {
                    PosaoID = x.PosaoID,
                    PoslodavacID = x.Posao.PoslodavacID,
                    Komentar = x.Komentar,
                    PosloprimaocID = x.PosloprimaocID,
                    RecenzijaID = x.RecenzijaID,
                    UserPoslodavac = x.Posao.Poslodavac.Korisnik.Username,
                    UserPosloprimaoc = x.Posloprimaoc.Korisnik.Username,
                })
                .ToListAsync(cancellationToken: cancellationToken);

            
          

            return new RecenzijaPreuzmiResponse
            {
                Recenzije = recenzije
            };
        }



    }
}
