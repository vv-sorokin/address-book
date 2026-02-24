

using AddressBook.Application.Common.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;

namespace AddressBook.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(IUserRepository users,IPasswordHasher hasher, CancellationToken ct)
    {
        // create default admin user for development

        var adminEmail = "admin@local";
        var adminPassword = "Admin123!";


        var exists = await users.ExistsByEmailAsync(adminEmail, ct);


        if (exists)
            return;

        var admin = new User(
            email: adminEmail,
            passwordHash: hasher.Generate("Admin123!"),
            role: "Admin"
        );

        await users.AddAsync(admin, ct);
        await users.SaveChangesAsync(ct);
    }
}
