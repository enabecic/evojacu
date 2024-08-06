using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using evojacu.Models;
using evojacu.Helpers; // Za pristup TokenHelper
using evojacu.Endpoints.Korisnik.Dodaj;

namespace evojacu.Services
{
    public class UserService
    {
        private readonly evojacuDBContext _context;

        public UserService(evojacuDBContext context)
        {
            _context = context;
        }

        public async Task RegisterUserAsync(KorisnikDodajRequest request)
        {
            var existingUserEmail = await _context.Korisnici
                .FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingUserUsername = await _context.Korisnici
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUserEmail != null || existingUserUsername != null)
            {
                throw new Exception("Korisnik sa ovim emailom ili korisničkim imenom već postoji.");
            }

            var noviObj = new Korisnik
            {
                Username = request.Username,
                Email = request.Email,
                Lozinka = request.Lozinka,
                Ime = request.Ime,
                Prezime = request.Prezime,
                Adresa = request.Adresa,
                Zanimanje = request.Zanimanje,
                Telefon = request.Telefon,
                VerificationToken = TokenHelper.GenerateToken(),
                TokenExpiry = DateTime.Now.AddHours(24)
            };

            _context.Korisnici.Add(noviObj);
            await _context.SaveChangesAsync();

            await SendVerificationEmail(noviObj.Email, noviObj.VerificationToken);
        }

        public async Task SendVerificationEmail(string email, string token)
        {
            var verificationLink = $"http://localhost:4200/korisnik/verify-email?token={token}";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("behramlamija@gmail.com", "Lamija Behram"),
                Subject = "Email Verification",
                Body = $"Molimo vas da kliknete na sledeći link kako biste verifikovali vašu email adresu: <a href='{verificationLink}'>Verifikuj Email</a>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("behramlamija@gmail.com", "raqt yxty nxno oiby");
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
                Console.WriteLine("Email uspešno poslat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom slanja email-a: {ex.Message}");
                throw;
            }
        }

        public async Task VerifyEmailAsync(string token)
        {
            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(k => k.VerificationToken == token && k.TokenExpiry > DateTime.Now);

            if (korisnik != null)
            {
                korisnik.VerificationToken = null;
                korisnik.TokenExpiry = null;

                _context.Korisnici.Update(korisnik);
                await _context.SaveChangesAsync();
            }
        }
    }
}
