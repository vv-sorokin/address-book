using AddressBook.Application.Common.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;

namespace AddressBook.Application.Contacts;

public class ContactsService: IContactsService
{
    private readonly IContactRepository _contacts;
    private readonly ICurrentUser _current;

    public ContactsService(IContactRepository contacts, ICurrentUser current)
    {
        _contacts = contacts;
        _current = current;
    }


    public async Task<IReadOnlyList<ContactDto>> GetMyAsync(CancellationToken ct)
    {
        EnsureAuth();

        var list = await _contacts.GetAllForUserAsync(_current.UserId, ct);
        return list.Select(ToDto).ToList();
    }


    public async Task<ContactDto> CreateAsync(CreateContactRequest request, CancellationToken ct)
    {
        EnsureAuth();

        var contact = new Contact(
            ownerUserId: _current.UserId,
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email,
            phone: request.Phone,
            address: request.Address
        );

        await _contacts.AddAsync(contact, ct);

        await _contacts.SaveChangesAsync(ct);


        return ToDto(contact);
    }

    public async Task<ContactDto> UpdateAsync(Guid id, UpdateContactRequest request, CancellationToken ct)
    {
        EnsureAuth();

        var contact = await _contacts.GetByIdAsync(id, ct);
        if (contact is null) throw new KeyNotFoundException("Contact not found.");

        if (contact.OwnerUserId != _current.UserId)
            throw new UnauthorizedAccessException();

        contact.Update(request.FirstName, request.LastName, request.Email, request.Phone, request.Address);
        await _contacts.UpdateAsync(contact, ct);
        await _contacts.SaveChangesAsync(ct);

        return ToDto(contact);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        EnsureAuth();

        var contact = await _contacts.GetByIdAsync(id, ct);
        if (contact is null) return;

        if (contact.OwnerUserId != _current.UserId)
            throw new UnauthorizedAccessException();

        await _contacts.DeleteAsync(contact, ct);
        await _contacts.SaveChangesAsync(ct);
    }

    private void EnsureAuth()
    {
        if (!_current.IsAuthenticated || _current.UserId == Guid.Empty)
            throw new UnauthorizedAccessException();
    }

    private static ContactDto ToDto(Contact c) =>
        new(c.Id, c.FirstName, c.LastName, c.Email, c.Phone, c.Address);




}
