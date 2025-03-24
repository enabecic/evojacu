using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
        public override async Task<PosaoPreuzmiResponse> Obradi([FromQuery] PosaoPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var ci = new CultureInfo("bs-BH");

            var poslovi = await _applicationDbContext.Poslovi.Where(x =>
            (request.OpisPosla == null || x.OpisPosla.ToLower().Contains(request.OpisPosla.ToLower())) &&
            (request.KategorijaID == null || x.Zadatak.KategorijaID == request.KategorijaID)
        )
               .Select(x => new PosaoPreuzmiResponsePosao()
               {
                   FazaPoslaID = x.FazaPoslaId,
                   NazivFazePosla = x.FazaPosla.Naziv,
                   Adresa = x.Adresa,
                   // JePonuda = x.JePonuda,
                   GradID = x.GradId,
                   VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                   KrajVremena = x.VrijemeIzvrsavanja.KrajVremena.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                   NazivGrada = x.Grad.Naziv,
                   OpisPosla = x.OpisPosla,
                   PoslodavacID = x.PoslodavacID,
                   UserName = x.Poslodavac.Korisnik.Username,
                   PosaoID = x.ZadatakID,
                   ZadatakStraniID = x.ZadatakStraniID,
                   NazivZadatka = x.Zadatak.Naziv,
                   DatumObjaveString = x.DatumObjave.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                   DatumObjave = x.DatumObjave,
                   Cijena = x.Cijena,
                   CijenaString = x.Cijena.ToString("N2", ci),
                   UkljucenGPS = x.UkljucenGPS,
                   NazivKategorije = x.Zadatak.Kategorija.Naziv,
                   jeOdabran = x.jeOdabran
               }).ToListAsync(cancellationToken: cancellationToken);


            return new PosaoPreuzmiResponse
            {
                Poslovi = poslovi
            };
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PosaoPreuzmiResponsePosao>> GetPosaoById(int id, CancellationToken cancellationToken = default)
        {
            var ci = new CultureInfo("bs-BH");

            var posao = await _applicationDbContext.Poslovi
                .Where(x => x.ZadatakID == id)
                .Select(x => new PosaoPreuzmiResponsePosao()
                {
                    FazaPoslaID = x.FazaPoslaId,
                    NazivFazePosla = x.FazaPosla.Naziv,
                    Adresa = x.Adresa,
                    GradID = x.GradId,
                    VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                    KrajVremena = x.VrijemeIzvrsavanja.KrajVremena.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                    NazivGrada = x.Grad.Naziv,
                    OpisPosla = x.OpisPosla,
                    PoslodavacID = x.PoslodavacID,
                    UserName = x.Poslodavac.Korisnik.Username,
                    PosaoID = x.ZadatakID,
                    ZadatakStraniID = x.ZadatakStraniID,
                    NazivZadatka = x.Zadatak.Naziv,
                    DatumObjaveString = x.DatumObjave.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                    DatumObjave = x.DatumObjave,
                    Cijena = x.Cijena,
                    CijenaString = x.Cijena.ToString("N2", ci),
                    UkljucenGPS = x.UkljucenGPS,
                    NazivKategorije = x.Zadatak.Kategorija.Naziv,
                    jeOdabran = x.jeOdabran,
                    korisnikId = x.korisnikId

                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (posao == null)
            {
                return NotFound();
            }

            return Ok(posao);
        }




        [HttpGet("{korisnikId}")]
        public async Task<ActionResult<PosaoPreuzmiResponsePosao>> GetPosaoByKorisnikId(int korisnikId, CancellationToken cancellationToken = default)
        {
            var ci = new CultureInfo("bs-BH");

            var posao = await _applicationDbContext.Poslovi
                .Where(x => x.korisnikId == korisnikId)
                .Select(x => new PosaoPreuzmiResponsePosao()
                {
                    FazaPoslaID = x.FazaPoslaId,
                    NazivFazePosla = x.FazaPosla.Naziv,
                    Adresa = x.Adresa,
                    GradID = x.GradId,
                    VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaId,
                    KrajVremena = x.VrijemeIzvrsavanja.KrajVremena.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                    NazivGrada = x.Grad.Naziv,
                    OpisPosla = x.OpisPosla,
                    PoslodavacID = x.PoslodavacID,
                    UserName = x.Poslodavac.Korisnik.Username,
                    PosaoID = x.ZadatakID,
                    ZadatakStraniID = x.ZadatakStraniID,
                    NazivZadatka = x.Zadatak.Naziv,
                    DatumObjaveString = x.DatumObjave.ToString("dd.MM.yyyy HH:mm:ss", new CultureInfo("bs-BH")),
                    DatumObjave = x.DatumObjave,
                    Cijena = x.Cijena,
                    CijenaString = x.Cijena.ToString("N2", ci),
                    UkljucenGPS = x.UkljucenGPS,
                    NazivKategorije = x.Zadatak.Kategorija.Naziv,
                    jeOdabran = x.jeOdabran,
                    korisnikId=x.korisnikId
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
