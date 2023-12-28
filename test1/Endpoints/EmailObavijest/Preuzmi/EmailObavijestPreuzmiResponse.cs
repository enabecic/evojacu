namespace evojacu.Endpoints.EmailObavijest.Preuzmi
{
    public class EmailObavijestPreuzmiResponse
    {
       public List<EmailObavijestPreuzmiResponseEmail> Emailovi { get; set; }

    }

    public class EmailObavijestPreuzmiResponseEmail
    {
        public int EmailID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumSlanja { get; set; }


        public int PoslodavacID { get; set; }
        public int KorisnikID { get; set; }
        public string NazivKompanije { get; set; }


        public int PosloprimaocID { get; set; }
        public int KorisnikIDPosloprimaoc { get; set; }
        public string Strucnost { get; set; }
    }


}
