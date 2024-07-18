using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Korisnik.Dodaj
{
    [Route("korisnik-dodaj")]
    public class KorisnikDodajEndpoint : MyBaseEndpoint<KorisnikDodajRequest, KorisnikDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<KorisnikDodajResponse> Obradi([FromBody] KorisnikDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Korisnik
            {
                Username = request.Username,
                Email = request.Email,
                Lozinka = request.Lozinka,
                Ime = request.Ime,
                Prezime= request.Prezime,
                Adresa= request.Adresa,
                Zanimanje= request.Zanimanje,
                Telefon = request.Telefon
            };
            _applicationDbContext.Korisnici.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return new KorisnikDodajResponse
            {
                KorisnikID = noviObj.KorisnikID
            };
        }
    }
}