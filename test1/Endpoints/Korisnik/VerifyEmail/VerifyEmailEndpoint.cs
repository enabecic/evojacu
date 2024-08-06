using Microsoft.AspNetCore.Mvc;
using evojacu.Services;
using evojacu.Endpoints.Korisnik.VerifyEmail;
using System.Threading.Tasks;

namespace evojacu.Endpoints.Korisnik.VerifyEmail
{
    [Route("korisnik")]
    [ApiController]
    public class VerifyEmailEndpoint : ControllerBase
    {
        private readonly UserService _userService;

        public VerifyEmailEndpoint(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new VerifyEmailResponse
                {
                    Success = false,
                    Message = "Token je obavezan."
                });
            }

            try
            {
                await _userService.VerifyEmailAsync(request.Token);
                return Ok(new VerifyEmailResponse
                {
                    Success = true,
                    Message = "Email uspešno verifikovan."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new VerifyEmailResponse
                {
                    Success = false,
                    Message = "Greška prilikom verifikacije emaila."
                });
            }
        }
    }
}
