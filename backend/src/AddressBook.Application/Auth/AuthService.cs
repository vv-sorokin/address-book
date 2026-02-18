
using AddressBook.Application.Common.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;

namespace AddressBook.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenGenerator _tokens;
    public AuthService(IUserRepository users, IPasswordHasher hasher, ITokenGenerator tokens)
    {
        _users = users;
        _hasher = hasher;
        _tokens = tokens;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        var email = request.Email?.Trim();
        var password = request.Password?.Trim();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Email and password are required.");

        var user = await _users.GetByEmailAsync(email, ct);
        if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var token = _tokens.Generate(user);
        return new AuthResponse(token, user.Id, user.Email, user.Role);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        var email = request.Email?.Trim();
        var password = request.Password?.Trim();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Email and password are required.");

        var exists = await _users.ExistsByEmailAsync(email, ct);
        if (exists)
            throw new InvalidOperationException("User already exists.");

        var user = new User(
            email: email,
            passwordHash: _hasher.Generate(request.Password),
            role: "User"
        );

        await _users.AddAsync(user, ct);
        await _users.SaveChangesAsync(ct);


        var token = _tokens.Generate(user);
        return new AuthResponse(token, user.Id, user.Email, user.Role);
    }
}
