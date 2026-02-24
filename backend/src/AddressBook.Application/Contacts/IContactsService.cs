
namespace AddressBook.Application.Contacts;

public interface IContactsService
{
    Task<IReadOnlyList<ContactDto>> GetMyAsync(CancellationToken ct);
    Task<ContactDto> CreateAsync(CreateContactRequest request, CancellationToken ct);
    Task<ContactDto> UpdateAsync(Guid id, UpdateContactRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
