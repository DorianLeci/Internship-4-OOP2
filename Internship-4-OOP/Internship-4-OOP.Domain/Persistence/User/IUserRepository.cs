using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Entities.Users;

public interface IUserRepository:IRepository<User,int>
{
    Task<User> GetById(int id);
    Task<bool>ExistsByEmailAsync(string email);
    Task<bool>ExistsByUsernameAsync(string username);
    Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance);
    
}
