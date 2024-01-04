namespace evojacu.Endpoints.Recenzija.Preuzmi
{
    public class RecenzijaPreuzmiResponse
    {
        public List<RecenzijaPreuzmiResponseRecenzija> Recenzije { get; set; }
    }

    public class RecenzijaPreuzmiResponseRecenzija
    {
        public int RecenzijaID { get; set; }

        public int PosaoID { get; set; }
        public string OpisPosla { get; set; }

        public int PoslodavacID { get; set; }
        public string UserPoslodavac { get; set; }

        public int PosloprimaocID { get; set; }
        public string UserPosloprimaoc { get; set; }

        public string Komentar { get; set; }
        public double Ocjena { get; set; }
    }
}
