namespace evojacu.Endpoints.Recenzija.Update
{
    public class RecenzijaUpdateRequest
    {
        public int RecenzijaID { get; set; }
        public string Komentar { get; set; }
        public double Ocjena { get; set; }
    }
}
