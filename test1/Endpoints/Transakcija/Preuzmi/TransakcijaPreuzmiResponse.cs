namespace evojacu.Endpoints.Transakcija.Preuzmi
{
    public class TransakcijaPreuzmiResponse
    {
        public List<TransakcijaPreuzmiResponseTransakcija> Transakcije { get; set; }
    }


    public class TransakcijaPreuzmiResponseTransakcija
    {
        public int TransakcijaID { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VrijemeTransakcije { get; set; }


        public int NacinPlacanjaId { get; set; }
        public string TipPlacanja { get; set; }
        public string? BrojKartice { get; set; }
        public DateTime DatumIsteka { get; set; }


        public int StanjePlacanjaId { get; set; }
        public string OpisStanja { get; set; }


        public string Opis { get; set; }


        public int PosaoID { get; set; }
        public string OpisPosla { get; set; }
        public string Adresa { get; set; }



        public int PoslodavacID { get; set; }
        public string NazivKompanije { get; set; }
    }
}
