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
            var poslovi = await _applicationDbContext.Poslovi.Where(x =>
            (request.OpisPosla == null || x.OpisPosla.ToLower().Contains(request.OpisPosla.ToLower())) &&
            (request.KategorijaID == null || x.Zadatak.KategorijaID == request.KategorijaID) // Dodaj ovaj filter
        )
               .Select(x => new PosaoPreuzmiResponsePosao()
               {
                   FazaPoslaID = x.FazaPoslaId,
                   NazivFazePosla = x.FazaPosla.Naziv,
                   Adresa = x.Adresa,
                  // JePonuda = x.JePonuda,
                   GradID = x.GradId,
                   VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                   KrajVremena=x.VrijemeIzvrsavanja.KrajVremena,
                   NazivGrada = x.Grad.Naziv,
                   OpisPosla = x.OpisPosla,
                   PoslodavacID = x.PoslodavacID,
                   UserName = x.Poslodavac.Korisnik.Username,
                   PosaoID = x.ZadatakID,
                   ZadatakStraniID= x.ZadatakStraniID,
                   NazivZadatka=x.Zadatak.Naziv,
                   DatumObjave=x.DatumObjave, 
                   Cijena=x.Cijena,
                   UkljucenGPS=x.UkljucenGPS,
                   NazivKategorije=x.Zadatak.Kategorija.Naziv
               }).ToListAsync(cancellationToken: cancellationToken);


            return new PosaoPreuzmiResponse
            {
                Poslovi = poslovi
            };
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PosaoPreuzmiResponsePosao>> GetPosaoById(int id, CancellationToken cancellationToken = default)
        {
            var posao = await _applicationDbContext.Poslovi
                .Where(x => x.ZadatakID == id)
                .Select(x => new PosaoPreuzmiResponsePosao()
                {
                    FazaPoslaID = x.FazaPoslaId,
                    NazivFazePosla = x.FazaPosla.Naziv,
                    Adresa = x.Adresa,
                    GradID = x.GradId,
                    VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                    KrajVremena = x.VrijemeIzvrsavanja.KrajVremena,
                    NazivGrada = x.Grad.Naziv,
                    OpisPosla = x.OpisPosla,
                    PoslodavacID = x.PoslodavacID,
                    UserName = x.Poslodavac.Korisnik.Username,
                    PosaoID = x.ZadatakID,
                    ZadatakStraniID = x.ZadatakStraniID,
                    NazivZadatka = x.Zadatak.Naziv,
                    DatumObjave = x.DatumObjave,
                    Cijena = x.Cijena,
                    UkljucenGPS= x.UkljucenGPS
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (posao == null)
            {
                return NotFound();
            }

            return Ok(posao);
        }
    }
}
