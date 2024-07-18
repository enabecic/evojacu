namespace evojacu.Endpoints.VrijemeIzvrsavanja.Preuzmi
{
    public class VrijemeIzvrsavanjaPreuzmiResponse
    {
        public List<VrijemeIzvrsavanjaPreuzmiResponseVremenaIzvrsavanja> VremenaIzvrsavanja { get; set; }
    }

    public class VrijemeIzvrsavanjaPreuzmiResponseVremenaIzvrsavanja
    {
        public int VrijemeIzvrsavanjaID { get; set; }
        public DateTime KrajVremena { get; set; }
    }
}
