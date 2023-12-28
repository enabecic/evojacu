namespace evojacu.Endpoints.AdminPanel.Preuzmi
{
    public class AdminPanelPreuzmiResponse
    {
        public List<AdminPanelPreuzmiResponseAdmin> Admini { get; set; }    
    }

    public class AdminPanelPreuzmiResponseAdmin
    {
        public int AdminID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }
    
}
