using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.EmailObavijest.Update
{
    [Route("EmailObavijest-update")]
    public class EmailObavijestUpdateEndpoint : MyBaseEndpoint<EmailObavijestUpdateRequest, EmailObavijestUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public EmailObavijestUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<EmailObavijestUpdateResponse> Obradi([FromBody]EmailObavijestUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var email=_applicationDbContext.EmailObavijesti.FirstOrDefault(x=>x.EmailID==request.EmailID);

            if (email == null)
            {
                throw new Exception("Ne postoji email sa ID = " + request.EmailID);
            }

            email.Naslov = request.Naslov;
            email.Sadrzaj = request.Sadrzaj;
            email.DatumSlanja=request.DatumSlanja;

            await _applicationDbContext.SaveChangesAsync();


            return new EmailObavijestUpdateResponse
            {
                 EmailID=email.EmailID
            };
        }
    }


}
