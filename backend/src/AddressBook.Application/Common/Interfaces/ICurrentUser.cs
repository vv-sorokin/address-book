
namespace AddressBook.Application.Common.Interfaces;

public interface ICurrentUser
{
    Guid UserId { get; }
    string? Role { get; }
    bool IsAuthenticated { get; }
}
