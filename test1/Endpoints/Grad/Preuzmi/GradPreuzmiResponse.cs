namespace evojacu.Endpoints.Grad.Preuzmi
{
    public class GradPreuzmiResponse
    {
        public List<GradPreuzmiResponseGrad> Gradovi { get; set; }
    }

    public class GradPreuzmiResponseGrad
    {
        public int GradID { get; set; }
        public string Naziv { get; set; }
    }
}
