namespace evojacu.Endpoints.EmailObavijest.Dodaj
{
    public class EmailObavijestDodajRequest
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumSlanja { get; set; }
        public int PoslodavacID { get; set; }
        public int PosloprimaocID { get; set; }
    }
}
