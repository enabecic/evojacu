namespace evojacu.Endpoints.OdabraniPosao.Update
{
    public class OdabraniPosaoUpdateRequest
    {
        public int OdabraniPosaoID { get; set; }
        public int PosloprimaocID { get; set; }
        public int PosaoID { get; set; }
        public DateTime DatumOdabira { get; set; }
    }
}
