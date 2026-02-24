
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using AddressBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Repositories;

public class ContactRepository :IContactRepository
{

    private readonly AppDbContext _db;

    public ContactRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _db.Contacts.SingleOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Contact>> GetAllForUserAsync(Guid ownerUserId, CancellationToken ct) =>
        await _db.Contacts
            .Where(x => x.OwnerUserId == ownerUserId)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync(ct);

    public async Task AddAsync(Contact contact, CancellationToken ct)
    {
        await _db.Contacts.AddAsync(contact, ct);
    }

    public Task UpdateAsync(Contact contact, CancellationToken ct)
    {
        _db.Contacts.Update(contact);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Contact contact, CancellationToken ct)
    {
        _db.Contacts.Remove(contact);
        return Task.CompletedTask;
    }
}
