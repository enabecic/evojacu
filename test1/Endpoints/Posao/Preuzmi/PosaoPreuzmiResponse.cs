﻿namespace evojacu.Endpoints.Posao.Preuzmi
{
    public class PosaoPreuzmiResponse
    {
        public List<PosaoPreuzmiResponsePosao> Poslovi { get; set; }
    }

    public class PosaoPreuzmiResponsePosao
    {
        public int PosaoID { get; set; }
        public int GradID { get; set; }
        public string NazivGrada { get; set; }
        public int VrijemeIzvrsavanjaID { get; set; }
        public DateTime KrajVremena { get; set; }
        public int PoslodavacID { get; set; }
        public string UserName { get; set; }
        public string OpisPosla { get; set; }
        public string Adresa { get; set; }
       // public bool JePonuda { get; set; }
        public int FazaPoslaID { get; set; }
        public string NazivFazePosla { get; set; }
        public int ZadatakStraniID { get; set; }
        public string NazivZadatka { get; set; }
        public DateTime DatumObjave { get; set; }
        public float Cijena { get; set; }
        public bool UkljucenGPS { get; set; }
    }
}
