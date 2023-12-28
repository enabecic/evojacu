namespace evojacu.Endpoints.Korisnik.Update
{
    public class KorisnikUpdateRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
    }
}
