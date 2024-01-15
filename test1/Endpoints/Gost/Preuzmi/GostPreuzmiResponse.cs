namespace evojacu.Endpoints.Gost.Preuzmi
{
    public class GostPreuzmiResponse
    {
        public List<GostPreuzmiResponseGost> Gosti { get; set; }
    }

    public class GostPreuzmiResponseGost
    {
        public int GostID { get; set; }
        public int BrojPosjeta { get; set; }
    }
}
