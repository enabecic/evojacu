﻿namespace evojacu.Endpoints.Kategorija.Preuzmi
{
    public class KategorijaPreuzmiResponse
    {
        public List<KategorijaPreuzmiResponseKategorija> Kategorije { get; set; }
    }

    public class KategorijaPreuzmiResponseKategorija
    {
        public int KategorijaID { get; set; }
        public string Naziv { get; set; }
    }

}
