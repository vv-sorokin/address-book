using AddressBook.Application.Common.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AddressBook.Api.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    public CurrentUser(IHttpContextAccessor http)
    {
        _http = http;
    }

    public bool IsAuthenticated =>
        _http.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public Guid UserId
    {
        get
        {
            var sub = _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? _http.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(sub, out var id) ? id : Guid.Empty;
        }
    }

    public string? Role =>
        _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);



}
