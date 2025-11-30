using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserRepository:IRepository<Entities.Users.User,int>
{
    Task<Entities.Users.User?> GetByIdAsync(int id);
    
    Task<IReadOnlyList<Entities.Users.User>> GetAllAsync();
    
    Task<bool>ExistsByEmailAsync(string email);
    Task<bool>ExistsByUsernameAsync(string username);
    Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance);
    
    Task<Entities.Users.User?> AuthenticateAsync(string username,string password);
    
    Task<Entities.Users.User?> GetByUsernameAndPasswordAsync(string username,string password);
    
    
}
