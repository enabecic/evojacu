namespace evojacu.Endpoints.VrstaPlacanja.Preuzmi
{
    public class VrstaPlacanjaPreuzmiResponse
    {
        public List<VrstaPlacanjaPreuzmiResponseVrsta> Vrste { get; set; }
    }


    public class VrstaPlacanjaPreuzmiResponseVrsta
    {

        public int VrstaPlacanjaID { get; set; }
        public string TipPlacanja { get; set; }
        public string? BrojKartice { get; set; }
        public DateTime DatumIsteka { get; set; }

    }
}
