
namespace AddressBook.Application.Auth;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password);
public record AuthResponse(
    string AuthToken,
    Guid UserId,
    string Email,
    string Role
);
