using Microsoft.AspNetCore.Mvc;
using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.VrijemeIzvrsavanja.Preuzmi
{
    [Route("VrijemeIzvrsavanja-preuzmi")]

    public class VrijemeIzvrsavanjaPreuzmiEndpoint : MyBaseEndpoint<VrijemeIzvrsavanjaPreuzmiRequest, VrijemeIzvrsavanjaPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public VrijemeIzvrsavanjaPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<VrijemeIzvrsavanjaPreuzmiResponse> Obradi([FromQuery] VrijemeIzvrsavanjaPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var vremenaIzvrsavanja = await _applicationDbContext.VremenaIzvrsavanja
                .Select(x => new VrijemeIzvrsavanjaPreuzmiResponseVremenaIzvrsavanja()
                {
                    VrijemeIzvrsavanjaID = x.VrijemeIzvrsavanjaID,
                    KrajVremena = x.KrajVremena
                }).ToListAsync(cancellationToken: cancellationToken);

            return new VrijemeIzvrsavanjaPreuzmiResponse
            {
                VremenaIzvrsavanja = vremenaIzvrsavanja
            }; 

            //var vremena = _applicationDbContext.VremenaIzvrsavanja.ToList();

            //var vrijeme = vremena.FirstOrDefault(k =>  k.KrajVremena == request.KrajnjeVr);


            //if (vremena != null)
            //{
            //    Console.WriteLine("Vrijeme je pronađeno!");
            //    throw new Exception("Postojece vrijeme");


            //}
            //else
            //{
            //    Console.WriteLine("Vrijeme nije pronađeno!");
            //    throw new Exception("Ne postoji vrijeme");
            //}


        }


    }
}
