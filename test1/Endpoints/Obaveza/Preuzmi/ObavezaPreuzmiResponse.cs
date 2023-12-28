namespace evojacu.Endpoints.Obaveza.Preuzmi
{
    public class ObavezaPreuzmiResponse
    {
       public  List<ObavezaPreuzmiResponseObaveza> Obaveze { get; set; }
    }

    public class ObavezaPreuzmiResponseObaveza
    {
        public int ObavezaID { get; set; }


        public int PosloprimaocID { get; set; }
        public int KorisnikId { get; set; }
        public string Strucnost { get; set; }


        public string Opis { get; set; }
        public DateTime DatumRokaIzvrsenja { get; set; }


        public int PrioritetID { get; set; }
        public string NazivPrioriteta { get; set; }
        public string OpisPrioriteta { get; set; }
    }



}
