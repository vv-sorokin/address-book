using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("contacts")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {


    }
}
