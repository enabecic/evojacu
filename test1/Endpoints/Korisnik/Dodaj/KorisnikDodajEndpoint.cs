using Microsoft.AspNetCore.Mvc;
using evojacu.Models;
using Microsoft.EntityFrameworkCore;
using evojacu.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using evojacu.Models;
using evojacu.Helpers;

namespace evojacu.Endpoints.Korisnik.Dodaj
{
        [Route("korisnik")]
        [ApiController]
        public class KorisnikDodajEndpoint : ControllerBase
        {
            private readonly evojacuDBContext _applicationDbContext;
            private readonly ILogger<KorisnikDodajEndpoint> _logger;
            private readonly UserService _userService;
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env; // Dodano

            public KorisnikDodajEndpoint(
                evojacuDBContext applicationDbContext,
                ILogger<KorisnikDodajEndpoint> logger,
                UserService userService,
                IConfiguration configuration,
                IWebHostEnvironment env) // Dodano
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
                _userService = userService;
                _configuration = configuration;
                _env = env; // Inicijalizacija
            }

            public class LoginData
        {
            public string Email { get; set; }
            public string Lozinka { get; set; }
        }
        [HttpPost("dodaj")]
        public async Task<IActionResult> DodajKorisnika([FromBody] KorisnikDodajRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Proveri da li korisnik sa istim email-om već postoji
                var existingUserEmail = await _applicationDbContext.Korisnici
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                var existingUserUsername = await _applicationDbContext.Korisnici
                    .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

                if (existingUserEmail != null)
                {
                    // Ako korisnik sa tim email-om već postoji, vrati odgovarajuću poruku
                    _logger.LogWarning("Registracija neuspešna. Korisnik sa email-om {Email} već postoji.", request.Email);
                    return Conflict(new { Message = "Korisnik sa ovim emailom već postoji." });
                }

                if (existingUserUsername != null)
                {
                    // Ako korisnik sa tim korisničkim imenom već postoji, vrati odgovarajuću poruku
                    _logger.LogWarning("Registracija neuspešna. Korisnik sa username-om {Username} već postoji.", request.Username);
                    return Conflict(new { Message = "Korisnik sa ovim korisničkim imenom već postoji." });
                }

                var verificationToken = TokenHelper.GenerateToken(); // Generiši token

                var noviObj = new evojacu.Models.Korisnik
                {
                    Username = request.Username,
                    Email = request.Email,
                    Lozinka = request.Lozinka,
                    Ime = request.Ime,
                    Prezime = request.Prezime,
                    Adresa = request.Adresa,
                    Zanimanje = request.Zanimanje,
                    Telefon = request.Telefon,
                    VerificationToken = verificationToken,
                    TokenExpiry = DateTime.Now.AddHours(24) // Token važi 24 sata
                };

                _applicationDbContext.Korisnici.Add(noviObj);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                // Pošaljite verifikacioni email
                await _userService.SendVerificationEmail(noviObj.Email, verificationToken);

                _logger.LogInformation("Korisnik uspješno dodan: {KorisnikID}", noviObj.KorisnikID);

                return Ok(new KorisnikDodajResponse
                {
                    KorisnikID = noviObj.KorisnikID
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom dodavanja korisnika.");
                return StatusCode(500, "Interna greška servera.");
            }
        }

        [HttpGet("provjeri-email")]
        public async Task<IActionResult> ProvjeriEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { Message = "Email je obavezan." });
            }

            var emailExists = await _applicationDbContext.Korisnici.AnyAsync(k => k.Email == email);
            return Ok(new { Exists = emailExists });
        }

        [HttpGet("provjeri-username")]
        public async Task<IActionResult> ProvjeriUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new { Message = "Korisničko ime je obavezno." });
            }

            var usernameExists = await _applicationDbContext.Korisnici.AnyAsync(k => k.Username == username);
            return Ok(new { Exists = usernameExists });
        }

        [HttpPost("prijava")]
        public async Task<IActionResult> PrijaviKorisnika([FromBody] LoginData loginData)
        {
            try
            {
                _logger.LogInformation("Početak prijavljivanja korisnika: {Email}", loginData.Email);

                var korisnik = await _applicationDbContext.Korisnici
                    .FirstOrDefaultAsync(k => k.Email == loginData.Email);

                if (korisnik == null || loginData.Lozinka != korisnik.Lozinka)
                {
                    _logger.LogWarning("Neuspjela prijava: Pogrešan email ili lozinka za {Email}.", loginData.Email);
                    return Unauthorized(new { Message = "Neispravan email ili lozinka." });
                }

                var token = GenerateJwtToken(korisnik);

                _logger.LogInformation("Korisnik {Email} uspješno prijavljen.", loginData.Email);

                return Ok(new
                {
                    success = true,
                    korisnik = new
                    {
                        korisnik.KorisnikID,
                        korisnik.Username,
                        korisnik.Email,
                        korisnik.Ime,
                        korisnik.Prezime,
                        korisnik.Adresa,
                        korisnik.Zanimanje,
                        korisnik.Telefon,
                        token
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom prijavljivanja korisnika {Email}.", loginData.Email);
                return StatusCode(500, new { Message = "Interna greška servera.", Error = ex.Message });
            }
        }



        private string GenerateJwtToken(evojacu.Models.Korisnik korisnik)
        {
            string key = _configuration["Jwt:Key"];
            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];

            _logger.LogInformation("Generisanje JWT tokena za korisnika {Email}", korisnik.Email);
            _logger.LogInformation("Issuer: {Issuer}, Audience: {Audience}", issuer, audience);

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT Key nije pronađen u konfiguraciji.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, korisnik.KorisnikID.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, korisnik.Email),
        new Claim(JwtRegisteredClaimNames.UniqueName, korisnik.Username),
        new Claim(ClaimTypes.Role, "User"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    

        [HttpGet("{korisnikID}")]
        public async Task<ActionResult<evojacu.Models.Korisnik>> GetKorisnik(int korisnikID)
        {
            var korisnik = await _applicationDbContext.Korisnici
                                          .FirstOrDefaultAsync(k => k.KorisnikID == korisnikID);

            if (korisnik == null)
            {
                return NotFound(); // Ako korisnik nije pronađen
            }

            return Ok(korisnik); // Vraća korisničke podatke
        }
    }
}




