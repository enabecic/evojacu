using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Korisnik.Update
{
    [Route("korisnik-update")]

    public class KorisnikUpdateEndpoint : MyBaseEndpoint<KorisnikUpdateRequest, KorisnikUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<KorisnikUpdateResponse> Obradi([FromQuery] KorisnikUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var korisnici = _applicationDbContext.Korisnici.FirstOrDefault(x => x.KorisnikID == request.Id);

            if (korisnici == null)
            {
                throw new Exception("Nema korisnika koji posjeduje ID -> " + request.Id);
            }

            korisnici.Username = request.Username;
            korisnici.Email = request.Email;
            korisnici.Lozinka = request.Lozinka;
            korisnici.Ime= request.Ime;
            korisnici.Prezime= request.Prezime;
            korisnici.Zanimanje= request.Zanimanje;
            korisnici.Telefon= request.Telefon;
            korisnici.Adresa= request.Adresa;

            await _applicationDbContext.SaveChangesAsync();

            return new KorisnikUpdateResponse
            {
                KorisnikId = korisnici.KorisnikID
            };
        }
    }

}


