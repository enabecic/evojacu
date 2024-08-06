using Microsoft.AspNetCore.Mvc;
using evojacu.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using evojacu.Helpers;
using evojacu.Services;

namespace evojacu.Endpoints.Korisnik.Dodaj
{
    [Route("korisnik")]
    [ApiController]
    public class KorisnikDodajEndpoint : ControllerBase
    {
        private readonly evojacuDBContext _applicationDbContext;
        private readonly ILogger<KorisnikDodajEndpoint> _logger;
        private readonly UserService _userService; // Dodano za slanje email-a

        public KorisnikDodajEndpoint(evojacuDBContext applicationDbContext, ILogger<KorisnikDodajEndpoint> logger, UserService userService)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _userService = userService; // Inicijalizacija UserService
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
        public async Task<IActionResult> PrijaviKorisnika([FromBody] LoginData loginData, CancellationToken cancellationToken = default)
        {
            try
            {
                var korisnik = await _applicationDbContext.Korisnici
                    .FirstOrDefaultAsync(k => k.Email == loginData.Email && k.Lozinka == loginData.Lozinka, cancellationToken);

                if (korisnik == null)
                {
                    _logger.LogWarning("Prijavljivanje nije uspelo za korisnika sa email-om {Email}.", loginData.Email);
                    return Unauthorized(new { Message = "Prijavljivanje nije uspelo. Proverite email i lozinku." });
                }

                _logger.LogInformation("Korisnik prijavljen: {KorisnikID}", korisnik.KorisnikID);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom prijavljivanja korisnika.");
                return StatusCode(500, "Interna greška servera.");
            }
        }
    }
}
