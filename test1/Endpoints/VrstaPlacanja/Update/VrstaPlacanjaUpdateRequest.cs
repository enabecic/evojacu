namespace evojacu.Endpoints.VrstaPlacanja.Update
{
    public class VrstaPlacanjaUpdateRequest
    {
        public int VrstaPlacanjaID { get; set; }
        public string TipPlacanja { get; set; }
        public string? BrojKartice { get; set; }
        public DateTime DatumIsteka { get; set; }
    }
}
