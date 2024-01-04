namespace evojacu.Endpoints.Zadatak.Preuzmi
{
    public class ZadatakPreuzmiResponse
    {
        public List<ZadatakPreuzmiResponseZadatak> Zadaci { get; set; }
    }

    public class ZadatakPreuzmiResponseZadatak
    {
        public int ZadatakId { get; set; }
        public int KategorijaID { get; set; }
        public string NazivKategorije { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
