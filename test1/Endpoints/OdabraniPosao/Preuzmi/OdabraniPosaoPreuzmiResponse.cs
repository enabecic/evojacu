namespace evojacu.Endpoints.OdabraniPosao.Preuzmi
{
    public class OdabraniPosaoPreuzmiResponse
    {
        public List<OdabraniPosaoPreuzmiResponseOdabraniPoslovi> odabraniPoslovi { get; set; }
    }

    public class OdabraniPosaoPreuzmiResponseOdabraniPoslovi
    {
        public int OdabraniPosaoID { get; set; }
        public int PosaoID { get; set; }
        public string NazivPosla { get; set; }
        public float Cijena { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public DateTime RokIzvrsavanja { get; set; }
        public bool GPS { get; set; }
        public int PosloprimaocID { get; set; }
        public string Username { get; set; }
        public DateTime DatumOdabira { get; set; }
        public string OpisPosla { get; set; }
        public DateTime DatumObjave { get; set; }
    }
}
