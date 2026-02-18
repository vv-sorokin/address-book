using AddressBook.Domain.Entities;

namespace AddressBook.Application.Common.Interfaces;

public interface ITokenGenerator
{
    string Generate(User user);
}
