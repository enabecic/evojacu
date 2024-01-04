namespace evojacu.Endpoints.Recenzija.Dodaj
{
    public class RecenzijaDodajRequest
    {
        public int PosaoID { get; set; }
        public int PoslodavacID { get; set; }
        public int PosloprimaocID { get; set; }
        public string Komentar { get; set; }
        public double Ocjena { get; set; }
    }
}
