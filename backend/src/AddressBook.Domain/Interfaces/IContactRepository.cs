using AddressBook.Domain.Entities;

namespace AddressBook.Domain.Interfaces;
public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Contact>> GetAllForUserAsync(Guid ownerUserId, CancellationToken ct);

    Task AddAsync(Contact contact, CancellationToken ct);
    Task UpdateAsync(Contact contact, CancellationToken ct);
    Task DeleteAsync(Contact contact, CancellationToken ct);


}