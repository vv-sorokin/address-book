using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using AddressBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<User> GetByEmailAsync(string email, CancellationToken ct) =>
        _db.Users.SingleOrDefaultAsync(x => x.Email == email, ct);

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct) =>
        _db.Users.AnyAsync(x => x.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _db.Users.AddAsync(user, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct) =>
        _db.SaveChangesAsync(ct);

}
