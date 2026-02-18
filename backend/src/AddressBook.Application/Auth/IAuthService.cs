
namespace AddressBook.Application.Auth;

public interface IAuthService
{
    Task<AuthResponse>  LoginAsync(LoginRequest request, CancellationToken ct);
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct);
}
