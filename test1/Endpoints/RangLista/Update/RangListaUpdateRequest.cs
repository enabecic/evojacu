namespace evojacu.Endpoints.RangLista.Update
{
    public class RangListaUpdateRequest
    {
        public int RangListaID { get; set; }
        public int Pozicija { get; set; }
        public int BrojZadataka { get; set; }
        public double ProsjecnaOcjena { get; set; }
    }
}
