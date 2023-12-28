namespace evojacu.Endpoints.Prioritet.Preuzmi
{
    public class PrioritetPreuzmiResponse
    {
        public List<PrioritetPreuzmiResponsePrioritet> Prioriteti { get; set; }
    }


    public class PrioritetPreuzmiResponsePrioritet
    {
        public int PrioritetID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }


}
