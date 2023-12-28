using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Transakcija.Preuzmi
{
    [Route("Transakcija-preuzmi")]
    public class TransakcijaPreuzmiEndpoint : MyBaseEndpoint<TransakcijaPreuzmiRequest, TransakcijaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public TransakcijaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public override async Task<TransakcijaPreuzmiResponse> Obradi([FromQuery]TransakcijaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var transakcije = await _applicationDbContext.Transakcije.Where(x => request.NazivLKompanije == null || x.Poslodavac.NazivKompanije.ToLower().StartsWith(request.NazivLKompanije.ToLower()))
                .Select(x => new TransakcijaPreuzmiResponseTransakcija()
                {
                    TransakcijaID=x.TransakcijaID,
                    Iznos=x.Iznos,
                    VrijemeTransakcije=x.VrijemeTransakcije,
                    NacinPlacanjaId=x.NacinPlacanjaId,
                    TipPlacanja=x.VrstaPlacanja.TipPlacanja,
                    BrojKartice=x.VrstaPlacanja.BrojKartice,
                    DatumIsteka=x.VrstaPlacanja.DatumIsteka,
                    StanjePlacanjaId=x.StanjePlacanjaId,
                    OpisStanja=x.StanjePlacanja.OpisStanja,
                    Opis=x.Opis,
                    PosaoID=x.PosaoID,
                    OpisPosla=x.Posao.OpisPosla,
                    Adresa=x.Posao.Adresa,
                     PoslodavacID=x.PoslodavacID,
                     NazivKompanije=x.Poslodavac.NazivKompanije
                }).ToListAsync(cancellationToken:cancellationToken);


            return new TransakcijaPreuzmiResponse
            {
                 Transakcije = transakcije
            };
        }
    }
}
