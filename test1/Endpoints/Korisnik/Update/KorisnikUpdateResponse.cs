﻿namespace evojacu.Endpoints.Korisnik.Update
{
    public class KorisnikUpdateResponse
    {
        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zanimanje { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string SlikaUrlVelika { get; set; }
        public string SlikaUrlMala { get; set; }
    }
}
