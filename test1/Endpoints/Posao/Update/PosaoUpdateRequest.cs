namespace evojacu.Endpoints.Posao.Update
{
    public class PosaoUpdateRequest
    {
        public int PosaoID { get; set; }
        public string OpisPosla { get; set; }
        public string Adresa { get; set; }
        public int VrijemeIzvrsavanjaID { get; set; }
        public int FazaPoslaID { get; set; }
    }
}
