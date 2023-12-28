using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.AdminPanel.Update
{

    [Route("AdminPanel-update")]
    public class AdminPanelUpdateEndpoint : MyBaseEndpoint<AdminPanelUpdateRequest, AdminPanelUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public AdminPanelUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }



        [HttpPost]
        public override async Task<AdminPanelUpdateResponse> Obradi([FromBody]AdminPanelUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var admin=_applicationDbContext.AdminPaneli.FirstOrDefault(x=>x.AdminID==request.AdminID);  

            if (admin==null)
            {
                throw new Exception("Ne postoji admin sa ID = "+request.AdminID);
            }

            admin.KorisnickoIme=request.KorisnickoIme;
            admin.Lozinka=request.Lozinka;


            await _applicationDbContext.SaveChangesAsync();

            return new AdminPanelUpdateResponse
            {
                 AdminID=admin.AdminID,
            };
        }
    }
}
