using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;


namespace evojacu.Endpoints.Grad.Update
{
    [Route("grad-update")]

    public class GradUpdateEndpoint : MyBaseEndpoint<GradUpdateRequest, GradUpdateResponse>
    {

        private readonly evojacuDBContext _applicationDbContext;

        public GradUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<GradUpdateResponse> Obradi([FromQuery] GradUpdateRequest request, CancellationToken cancellationToken = default)
        {

            var gradovi = _applicationDbContext.Gradovi.FirstOrDefault(x => x.GradID == request.Id);

            if (gradovi == null)
            {
                throw new Exception("Nema korisnika koji posjeduje ID -> " + request.Id);
            }

            gradovi.Naziv = request.Naziv;
            

            await _applicationDbContext.SaveChangesAsync();

            return new GradUpdateResponse
            {
                GradId = gradovi.GradID
            };
        }
    }
}
