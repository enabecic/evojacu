namespace evojacu.Endpoints.Obaveza.Update
{
    public class ObavezaUpdateRequest
    {
        public int ObavezaID { get; set; }
        public string Opis { get; set; }
        public DateTime DatumRokaIzvrsenja { get; set; }
    }
}
