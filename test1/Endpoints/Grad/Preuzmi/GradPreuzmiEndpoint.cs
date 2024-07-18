using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

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

            var gradovi = await _applicationDbContext.Gradovi.Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new GradPreuzmiResponseGrad()
                {
                    Naziv = x.Naziv,
                    GradID = x.GradID
                }).ToListAsync(cancellationToken: cancellationToken);

            return new GradPreuzmiResponse
            {
                Gradovi = gradovi
            };

            //var gradovi = _applicationDbContext.Gradovi.ToList();

            //var grad = gradovi.FirstOrDefault(k => k.Naziv == request.Naziv);

            //if (gradovi != null)
            //{
            //    Console.WriteLine("Grad je pronađen!");
            //    throw new Exception("Postojeci Grad");


            //}
            //else
            //{
            //    Console.WriteLine("Grad nije pronađen!");
            //    throw new Exception("Ne postoji Grad");
            //}


        }


    }
}
