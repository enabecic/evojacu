namespace evojacu.Endpoints.Zadatak.Preuzmi
{
    public class ZadatakPreuzmiResponse
    {
        public List<ZadatakPreuzmiResponseZadatak> Zadaci { get; set; }
        public int UkupanBrojZadataka { get; set; }
        public int BrojStranica { get; set; }
    }

    public class ZadatakPreuzmiResponseZadatak
    {
        public int ZadatakId { get; set; }
        public int KategorijaID { get; set; }
        public string NazivKategorije { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int KorisnikId { get; set; }
    }
}
