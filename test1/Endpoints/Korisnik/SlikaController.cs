using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using evojacu.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace evojacu.Endpoints.Korisnik
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlikaController : ControllerBase
    {
        private readonly evojacuDBContext _context;

        public SlikaController(evojacuDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("postavi-sliku/{korisnikId}")]
        public async Task<IActionResult> PostaviSliku(int korisnikId, [FromForm] SlikaUploadModel model)
        {
            if (model == null || model.Slika == null)
            {
                return BadRequest("Nema slike za upload.");
            }

            try
            {
                var korisnik = await _context.Korisnici.FindAsync(korisnikId);

                if (korisnik == null)
                {
                    return NotFound($"Korisnik s ID-em {korisnikId} nije pronađen.");
                }

                // Provjeri tip MIME slike
                var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/jpg" };
                if (!allowedMimeTypes.Contains(model.Slika.ContentType))
                {
                    return BadRequest("Nedozvoljeni format slike.");
                }

                // Konvertiraj sliku u bajt niz
                using (var ms = new MemoryStream())
                {
                    await model.Slika.CopyToAsync(ms);
                    var slikaBytes = ms.ToArray();

                    // Ažuriraj sliku korisnika
                    korisnik.Slika = slikaBytes; // Čuvanje kao Base64 string
                    korisnik.SlikaMimeType = model.Slika.ContentType;
                    await _context.SaveChangesAsync();

                    return Ok("Slika je uspješno postavljena.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška na serveru: {ex.Message}");
            }
        }
    }

    public class SlikaUploadModel
    {
        public IFormFile Slika { get; set; }
    }
}
