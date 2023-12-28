namespace evojacu.Endpoints.RangLista.Preuzmi
{
    public class RangListaPreuzmiResponse
    {
      public List<RangListaPreuzmiResponseRang> Rangovi { get; set; }
    }

    public class RangListaPreuzmiResponseRang
    {
        public int RangListaID { get; set; }


        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }


        public int Pozicija { get; set; }
        public int BrojZadataka { get; set; }
        public double ProsjecnaOcjena { get; set; }
    }



}
