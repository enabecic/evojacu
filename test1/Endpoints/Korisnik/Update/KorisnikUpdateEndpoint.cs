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

            await _applicationDbContext.SaveChangesAsync();//izvrašva se "insert into Ispit value ...."

            return new KorisnikUpdateResponse
            {
                KorisnikId = korisnici.KorisnikID
            };
        }
    }

}


