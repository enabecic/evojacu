using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Grad.Obrisi
{
    
    
        [Route("Grad-obrisi")]

        public class GradObrisiEndpoint : MyBaseEndpoint<GradObrisiRequest, GradObrisiResponse>
        {

            private readonly evojacuDBContext _applicationDbContext;

            public GradObrisiEndpoint(evojacuDBContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            [HttpDelete]
            public override async Task<GradObrisiResponse> Obradi([FromQuery] GradObrisiRequest request, CancellationToken cancellationToken = default)
            {

                var gradovi = _applicationDbContext.Gradovi.FirstOrDefault(x => x.GradID == request.GradID);

                if (gradovi == null)
                {
                    throw new Exception("Nema gradova koji posjeduje ID -> " + request.GradID);
                }

                _applicationDbContext.Remove(gradovi);
                await _applicationDbContext.SaveChangesAsync();

                return new GradObrisiResponse
                {

                };
            }
        }
    
}
