namespace evojacu.Endpoints.EmailObavijest.Update
{
    public class EmailObavijestUpdateRequest
    {
        public int EmailID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumSlanja { get; set; }
    }
}
