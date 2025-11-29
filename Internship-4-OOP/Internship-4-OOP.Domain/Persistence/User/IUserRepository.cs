using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserRepository:IRepository<Entities.Users.User,int>
{
    Task<Entities.Users.User> GetById(int id);
    Task<bool>ExistsByEmailAsync(string email);
    Task<bool>ExistsByUsernameAsync(string username);
    Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance);
    
}
