using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.AdminPanel.Obrisi
{
    [Route("AdminPanel-obrisi")]
    public class AdminPanelObrisiEndpoint : MyBaseEndpoint<AdminPanelObrisiRequest, AdminPanelObrisiResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public AdminPanelObrisiEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<AdminPanelObrisiResponse> Obradi([FromQuery]AdminPanelObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var admin=_applicationDbContext.AdminPaneli.FirstOrDefault(x=>x.AdminID== request.AdminID); 

            if (admin == null)
            {
                throw new Exception("Ne postoji admin sa ID= " + request.AdminID);
            }

            _applicationDbContext.AdminPaneli.Remove(admin);
            await _applicationDbContext.SaveChangesAsync();

            return new AdminPanelObrisiResponse
            {

            };
        }
    }
}
