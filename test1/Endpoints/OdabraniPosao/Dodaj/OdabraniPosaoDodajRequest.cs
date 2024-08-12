namespace evojacu.Endpoints.OdabraniPosao.Dodaj
{
    public class OdabraniPosaoDodajRequest
    {
        public int OdabraniPosaoID { get; set; }
        public int PosaoID { get; set; }
        public int PosloprimaocID { get; set; }
        public DateTime DatumOdabira { get; set; }
    }
}
