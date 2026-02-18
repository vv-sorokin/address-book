using AddressBook.Application.Common.Interfaces;
using AddressBook.Infrastructure.Auth;
using AddressBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AddressBook.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenGenerator _tokens;
        private readonly IPasswordHasher _hasher;

        public AuthController(AppDbContext db, JwtTokenGenerator tokens, IPasswordHasher hasher)
        {
            _db = db;
            _tokens = tokens;
            _hasher = hasher;
        }

        public record LoginRequest(string Email, string Password);
        public record LoginResponse(string AccessToken);

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request , CancellationToken ct)
        {
            var email = request.Email?.Trim();
            var password = request.Password?.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email and password are required. ");

            var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == email, ct);

            if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            var token = _tokens.Generate(user);
            return Ok(token);
        }
    }
}
