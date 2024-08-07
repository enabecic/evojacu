﻿using Microsoft.AspNetCore.Mvc;

using evojacu.Helpers;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Poslodavac.Preuzmi
{
    [Route("Poslodavac-preuzmi")]
    public class PoslodavacPreuzmiEndpoint : MyBaseEndpoint<PoslodavacPreuzmiRequest, PoslodavacPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public PoslodavacPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        public override async Task<PoslodavacPreuzmiResponse> Obradi([FromQuery] PoslodavacPreuzmiRequest request, CancellationToken cancellationToken = default)
        {

            var poslodavci = await _applicationDbContext.Poslodavci
                .Select(x => new PoslodavacPreuzmiResponsePoslodavci()
                {
                    KorisnikID = x.KorisnikId,
                    PoslodavacID= x.PoslodavacID,
                    NazivKompanije = x.NazivKompanije,
                    UserName = x.Korisnik.Username
                }).ToListAsync(cancellationToken: cancellationToken);

            return new PoslodavacPreuzmiResponse
            {
                Poslodavci = poslodavci
            };

            //var poslodavci = _applicationDbContext.Poslodavci.ToList();

            //var poslodavcilista = poslodavci.FirstOrDefault(o => o.PoslodavacID == request.PoslodavacID);

            //if (poslodavcilista == null)
            //{

            //    throw new Exception("Ne postoji poslodavac sa tim ID-om");
            //}

            //return new PoslodavacPreuzmiResponse
            //{
                
            //    KorisnikId = poslodavcilista.KorisnikId,
            //    NazivKompanije=poslodavcilista.NazivKompanije
            //};
        }

    }
}
