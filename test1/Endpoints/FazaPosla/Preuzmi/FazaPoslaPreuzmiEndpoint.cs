using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.FazaPosla.Preuzmi
{
    [Route("FazaPosla-preuzmi")]

    public class FazaPoslaPreuzmiEndpoint : MyBaseEndpoint<FazaPoslaPreuzmiRequest, FazaPoslaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public FazaPoslaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<FazaPoslaPreuzmiResponse> Obradi([FromQuery] FazaPoslaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var fazePoslova = await _applicationDbContext.FazePoslova.Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new FazaPoslaPreuzmiResponseFazaPosla()
                {
                     FazaPoslaID= x.FazaPoslaID,
                      Naziv= x.Naziv,
                       Opis= x.Opis
                }).ToListAsync(cancellationToken: cancellationToken);

            return new FazaPoslaPreuzmiResponse
            {
                FazePoslova = fazePoslova
            };
            //var fazeposlova = _applicationDbContext.FazePoslova.ToList();

            //var fazaposla = fazeposlova.FirstOrDefault(k => k.Naziv == request.Naziv);

            //if (fazeposlova != null)
            //{
            //    Console.WriteLine("Faza posla je pronađena!");
            //    throw new Exception("Postojeca faza posla");


            //}
            //else
            //{
            //    Console.WriteLine("Faza posla nije pronađena!");
            //    throw new Exception("Ne postoji faza posla");
            //}


        }


    }
}
