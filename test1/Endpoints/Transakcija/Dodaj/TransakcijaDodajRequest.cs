namespace evojacu.Endpoints.Transakcija.Dodaj
{
    public class TransakcijaDodajRequest
    {
        public decimal Iznos { get; set; }
        public DateTime VrijemeTransakcije { get; set; }
        public int NacinPlacanjaId { get; set; }
        public int StanjePlacanjaId { get; set; }
        public string Opis { get; set; }
        public int PosaoID { get; set; }
        public int PoslodavacID { get; set; }
    }
}
