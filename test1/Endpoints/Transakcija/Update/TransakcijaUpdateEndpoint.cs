using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Transakcija.Update
{
    [Route("Transakcija-update")]
    public class TransakcijaUpdateEndpoint : MyBaseEndpoint<TransakcijaUpdateRequest, TransakcijaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public TransakcijaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }


        [HttpPost]
        public override async Task<TransakcijaUpdateResponse> Obradi([FromBody]TransakcijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var transakcija = _applicationDbContext.Transakcije.FirstOrDefault(x => x.TransakcijaID == request.TransakcijaID);

            if (transakcija == null)
            {
                throw new Exception("Ne postoji transakcija sa ID = " + request.TransakcijaID);
            }

            transakcija.Iznos=request.Iznos;
            transakcija.VrijemeTransakcije = request.VrijemeTransakcije;
            transakcija.Opis = request.Opis;

            await _applicationDbContext.SaveChangesAsync();


            return new TransakcijaUpdateResponse
            {
                 TransakcijaID=transakcija.TransakcijaID
            };
        }
    }
}
