namespace evojacu.Endpoints.FazaPosla.Preuzmi
{
    public class FazaPoslaPreuzmiResponse
    {
        public List<FazaPoslaPreuzmiResponseFazaPosla> FazePoslova { get; set; }
    }

    public class FazaPoslaPreuzmiResponseFazaPosla
    {
        public int FazaPoslaID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
