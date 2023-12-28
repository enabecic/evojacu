namespace evojacu.Endpoints.Transakcija.Update
{
    public class TransakcijaUpdateRequest
    {
        public int TransakcijaID { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VrijemeTransakcije { get; set; }
        public string Opis { get; set; }
    }
}
