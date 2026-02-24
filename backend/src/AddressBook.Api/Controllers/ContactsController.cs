using AddressBook.Application.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers;

[Route("contacts")]
[ApiController]
[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _contacts;

    public ContactsController(IContactsService contacts)
    {
        _contacts = contacts;
    }

    [HttpGet]
    public async Task<IActionResult> GetMy(CancellationToken ct)
        => Ok(await _contacts.GetMyAsync(ct));


    [HttpPost]
    public async Task<IActionResult> Create(CreateContactRequest request, CancellationToken ct)
        => Ok(await _contacts.CreateAsync(request, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,  UpdateContactRequest request, CancellationToken ct)
        => Ok(await _contacts.UpdateAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _contacts.DeleteAsync(id, ct);
        return NoContent();
    }

}
