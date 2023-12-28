using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.AdminPanel.Dodaj
{
    [Route("AdminPanel-dodaj")]
    public class AdminPanelDodajEndpoint : MyBaseEndpoint<AdminPanelDodajRequest, AdminPanelDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public AdminPanelDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }


        [HttpPost]
        public override async Task<AdminPanelDodajResponse> Obradi([FromBody]AdminPanelDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.AdminPanel
            {
                 KorisnickoIme= request.KorisnickoIme,
                 Lozinka= request.Lozinka
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();


            return new AdminPanelDodajResponse
            {
                 AdminID=noviObj.AdminID
            };
        }
    }
}
