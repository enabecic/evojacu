﻿using Microsoft.AspNetCore.Mvc;

namespace evojacu.Endpoints.Korisnik.Preuzmi
{
    public class KorisnikPreuzmiRequest
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Lozinka { get; set; }
    }
}
