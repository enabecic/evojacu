using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Obaveza.Preuzmi
{
    [Route("Obaveza-preuzmi")]
    public class ObavezaPreuzmiEndpoint : MyBaseEndpoint<ObavezaPreuzmiRequest, ObavezaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ObavezaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }


        [HttpGet]
        public override async Task<ObavezaPreuzmiResponse> Obradi([FromQuery]ObavezaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var obaveze = await _applicationDbContext.Obaveze.Where(x => request.NazivPrioriteta == null || x.Prioritet.Naziv.ToLower().StartsWith(request.NazivPrioriteta.ToLower()))
                .Select(x => new ObavezaPreuzmiResponseObaveza()
                {
                     ObavezaID=x.ObavezaID,
                     PosloprimaocID=x.PosloprimaocID,
                    KorisnikId = x.Posloprimaoc.KorisnikId,
                    Strucnost = x.Posloprimaoc.Strucnost,
                    Opis = x.Opis,
                    DatumRokaIzvrsenja = x.DatumRokaIzvrsenja,
                    PrioritetID = x.PrioritetID,
                      NazivPrioriteta =x.Prioritet.Naziv,
                      OpisPrioriteta=x.Prioritet.Opis

                }).ToListAsync(cancellationToken: cancellationToken);


            return new ObavezaPreuzmiResponse
            {
                Obaveze = obaveze
            };
        }
    }
}
