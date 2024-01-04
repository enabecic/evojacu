namespace evojacu.Endpoints.Posao.Dodaj
{
    public class PosaoDodajRequest
    {
        public int VrijemeIzvrsavanjaID { get; set; }
        public int GradID { get; set; }
        public int FazaPoslaID { get; set; }
        public string OpisPosla { get; set; }
        public bool JePonuda { get; set; }
        public string Adresa { get; set; }
        public int KorisnikID { get; set; }
        public int ZadatakStraniID { get; set; }

    }
}
