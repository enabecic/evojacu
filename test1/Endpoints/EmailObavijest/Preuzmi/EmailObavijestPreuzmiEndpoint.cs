using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.EmailObavijest.Preuzmi
{
    [Route("EmailObavijest-preuzmi")]
    public class EmailObavijestPreuzmiEndpoint : MyBaseEndpoint<EmailObavijestPreuzmiRequest, EmailObavijestPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public EmailObavijestPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public override async Task<EmailObavijestPreuzmiResponse> Obradi([FromQuery]EmailObavijestPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var emailovi = await _applicationDbContext.EmailObavijesti.Where(x => request.Naslov == null || x.Naslov.ToLower().StartsWith(request.Naslov.ToLower()))
                .Select(x => new EmailObavijestPreuzmiResponseEmail()
                {
                     EmailID= x.EmailID,
                     Naslov= x.Naslov,
                     Sadrzaj=x.Sadrzaj,
                     DatumSlanja= x.DatumSlanja,
                     PoslodavacID= x.PoslodavacID,
                     KorisnikID=x.Poslodavac.KorisnikId,
                     NazivKompanije=x.Poslodavac.NazivKompanije,
                     PosloprimaocID=x.PosloprimaocID,
                     KorisnikIDPosloprimaoc=x.Posloprimaoc.KorisnikId,
                     Strucnost=x.Posloprimaoc.Strucnost
                   
                }).ToListAsync(cancellationToken:cancellationToken);

            return new EmailObavijestPreuzmiResponse
            {
                 Emailovi=emailovi
            };
        }
    }
}
