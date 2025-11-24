namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserRepository
{
    Task<User> GetById(int id);
}
