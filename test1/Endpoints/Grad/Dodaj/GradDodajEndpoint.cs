using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Grad.Dodaj
{
   
    
        [Route("Grad-dodaj")]
        public class GradDodajEndpoint : MyBaseEndpoint<GradDodajRequest, GradDodajResponse>
        {
            private readonly evojacuDBContext _applicationDbContext;

            public GradDodajEndpoint(evojacuDBContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            [HttpPost]
            public override async Task<GradDodajResponse> Obradi([FromBody] GradDodajRequest request, CancellationToken cancellationToken = default)
            {
                var noviObj = new evojacu.Models.Grad
                {
                   
                    Naziv = request.Naziv
                };
                _applicationDbContext.Gradovi.Add(noviObj);

                await _applicationDbContext.SaveChangesAsync();

                return new GradDodajResponse
                {
                    GradId = noviObj.GradID
                };
            }
        }
    
}
