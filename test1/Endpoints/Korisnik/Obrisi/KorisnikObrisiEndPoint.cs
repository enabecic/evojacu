using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Korisnik.Obrisi
{
    [Route("korisnik-obrisi")]

    public class KorisnikObrisiEndpoint : MyBaseEndpoint<KorisnikObrisiRequest, KorisnikObrisiResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public KorisnikObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<KorisnikObrisiResponse> Obradi([FromQuery] KorisnikObrisiRequest request, CancellationToken cancellationToken = default)
        {

            var korisnici = _applicationDbContext.Korisnici.FirstOrDefault(x => x.KorisnikID == request.KorisnikID);

            if (korisnici == null)
            {
                throw new Exception("Nema korisnika koji posjeduje ID -> " + request.KorisnikID);
            }

            _applicationDbContext.Remove(korisnici);
            await _applicationDbContext.SaveChangesAsync();

            return new KorisnikObrisiResponse
            {

            };
        }
    }

}

