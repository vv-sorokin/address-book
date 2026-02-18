using AddressBook.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request , CancellationToken ct)
        {
            try
            {
                var result = await _auth.LoginAsync(request, ct);
                return Ok(new
                {
                    accessToken = result.AuthToken,
                    user = new { id = result.UserId, email = result.Email, role = result.Role }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register( RegisterRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _auth.RegisterAsync(request, ct);
                return Ok(new
                {
                    accessToken = result.AuthToken,
                    user = new { id = result.UserId, email = result.Email, role = result.Role }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }

    
}
