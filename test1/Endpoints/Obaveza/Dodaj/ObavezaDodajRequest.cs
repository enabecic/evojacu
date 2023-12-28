namespace evojacu.Endpoints.Obaveza.Dodaj
{
    public class ObavezaDodajRequest
    {
        public int PosloprimaocID { get; set; }
        public string Opis { get; set; }
        public DateTime DatumRokaIzvrsenja { get; set; }
        public int PrioritetID { get; set; }
    }
}
