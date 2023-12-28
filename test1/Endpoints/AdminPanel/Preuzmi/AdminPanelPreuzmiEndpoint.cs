using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Endpoints.AdminPanel.Preuzmi
{
    [Route("AdminPanel-preuzmi")]
    public class AdminPanelPreuzmiEndpoint : MyBaseEndpoint<AdminPanelPreuzmiRequest, AdminPanelPreuzmiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public AdminPanelPreuzmiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }



        [HttpGet]
        public override async Task<AdminPanelPreuzmiResponse> Obradi([FromQuery]AdminPanelPreuzmiRequest request, CancellationToken cancellationToken = default)
        {
            var admin = await _applicationDbContext.AdminPaneli.Where(x => request.KorisnickoIme == null || x.KorisnickoIme.ToLower().StartsWith(request.KorisnickoIme.ToLower()))
                .Select(x => new AdminPanelPreuzmiResponseAdmin()
                {
                     AdminID= x.AdminID,
                      KorisnickoIme= x.KorisnickoIme,
                      Lozinka= x.Lozinka
                }).ToListAsync(cancellationToken:cancellationToken);


            return new AdminPanelPreuzmiResponse
            {
                 Admini= admin
            };
        }
    }
}
