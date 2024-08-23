﻿using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.Zadatak.Preuzmi
{
    [Route("Zadatak-preuzmi")]
    public class ZadatakPreuzmiEndpoint : MyBaseEndpoint<ZadatakPreuzmiRequest, ZadatakPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public override async Task<ZadatakPreuzmiResponse> Obradi([FromQuery]ZadatakPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var zadaci = await _applicationDbContext.Zadaci.Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new ZadatakPreuzmiResponseZadatak()
                {
                    ZadatakId = x.ZadatakId,
                    KategorijaID= x.KategorijaID,
                    Naziv= x.Naziv,
                    NazivKategorije=x.Kategorija.Naziv,
                    Opis= x.Opis
                  
                }).ToListAsync(cancellationToken: cancellationToken);


            return new ZadatakPreuzmiResponse
            {
                Zadaci = zadaci
            };
        }


        [HttpGet("paged")]
        public async Task<ActionResult<ZadatakPreuzmiResponse>> GetPaged([FromQuery] ZadatakPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            // Pronađi ukupni broj zadataka
            var ukupanBrojZadataka = await _applicationDbContext.Zadaci
                .Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .CountAsync(cancellationToken);

            // Izračunaj zadatke za trenutnu stranicu
            var zadaci = await _applicationDbContext.Zadaci
                .Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .OrderBy(x => x.ZadatakId)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ZadatakPreuzmiResponseZadatak
                {
                    ZadatakId = x.ZadatakId,
                    KategorijaID = x.KategorijaID,
                    Naziv = x.Naziv,
                    NazivKategorije = x.Kategorija.Naziv,
                    Opis = x.Opis
                })
                .ToListAsync(cancellationToken);

            // Izračunaj ukupan broj stranica
            var brojStranica = (int)Math.Ceiling((double)ukupanBrojZadataka / request.PageSize);

            return Ok(new ZadatakPreuzmiResponse
            {
                Zadaci = zadaci,
                UkupanBrojZadataka = ukupanBrojZadataka,
                BrojStranica = brojStranica
            });
        }


    }
}
