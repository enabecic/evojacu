using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.EmailObavijest.Obrisi
{
    [Route("EmailObavijest-obrisi")]
    public class EmailObavijestObrisiEndpoint : MyBaseEndpoint<EmailObavijestObrisiRequest, EmailObavijestObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public EmailObavijestObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<EmailObavijestObrisiResponse> Obradi([FromQuery]EmailObavijestObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var email=_applicationDbContext.EmailObavijesti.FirstOrDefault(x=>x.EmailID==request.EmailID);

            if (email==null)
            {
                throw new Exception("Ne postoji email sa ID = " + request.EmailID);
            }

            _applicationDbContext.Remove(email);
            await _applicationDbContext.SaveChangesAsync();

            return new EmailObavijestObrisiResponse
            {

            };
        }
    }
}
