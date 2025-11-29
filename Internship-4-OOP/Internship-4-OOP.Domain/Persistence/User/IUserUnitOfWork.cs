using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserUnitOfWork
{
    IUserRepository Repository { get; }
}