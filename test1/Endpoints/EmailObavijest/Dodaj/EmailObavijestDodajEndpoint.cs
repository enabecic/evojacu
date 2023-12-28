using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.EmailObavijest.Dodaj
{
    [Route("EmailObavijest-dodaj")]
    public class EmailObavijestDodajEndpoint : MyBaseEndpoint<EmailObavijestDodajRequest, EmailObavijestDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public EmailObavijestDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<EmailObavijestDodajResponse> Obradi([FromBody]EmailObavijestDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.EmailObavijest
            {
                 Naslov=request.Naslov,
                 Sadrzaj=request.Sadrzaj,
                 DatumSlanja=request.DatumSlanja,
                 PoslodavacID=request.PoslodavacID,
                 PosloprimaocID=request.PosloprimaocID
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();


            return new EmailObavijestDodajResponse
            {
                 EmailID=noviObj.EmailID
            };
        }
    }
}
