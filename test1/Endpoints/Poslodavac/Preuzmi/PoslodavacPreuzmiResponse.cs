namespace evojacu.Endpoints.Poslodavac.Preuzmi
{
    public class PoslodavacPreuzmiResponse
    {
        public List<PoslodavacPreuzmiResponsePoslodavci> Poslodavci { get; set; }
    }

    public class PoslodavacPreuzmiResponsePoslodavci
    {
        public int PoslodavacID { get; set; }
        public int KorisnikID { get; set; }
        public string? NazivKompanije { get; set; }
        public string UserName { get; set; }
    }
}
