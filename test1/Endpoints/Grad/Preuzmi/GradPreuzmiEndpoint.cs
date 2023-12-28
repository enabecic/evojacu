using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;

namespace evojacu.Endpoints.Grad.Preuzmi
{
    [Route("Grad-preuzmi")]

    public class GradPreuzmiEndpoint : MyBaseEndpoint<GradPreuzmiRequest, GradPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public GradPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<GradPreuzmiResponse> Obradi([FromQuery]GradPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var gradovi = _applicationDbContext.Gradovi.ToList();

            var grad = gradovi.FirstOrDefault(k => k.Naziv == request.Naziv);

            if (gradovi != null)
            {
                Console.WriteLine("Grad je pronađen!");
                throw new Exception("Postojeci Grad");


            }
            else
            {
                Console.WriteLine("Grad nije pronađen!");
                throw new Exception("Ne postoji Grad");
            }


        }


    }
}
