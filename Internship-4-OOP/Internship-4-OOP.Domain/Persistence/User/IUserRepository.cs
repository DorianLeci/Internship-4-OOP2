using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserRepository:IRepository<Entities.Users.User,int>
{
    Task<Entities.Users.User?> GetByIdAsync(int id);
    
    Task<IReadOnlyList<Entities.Users.User>> GetAllAsync();
    
    Task<bool>ExistsByEmailAsync(string email,int? excludeId = null);
    Task<bool>ExistsByUsernameAsync(string username,int? excludeId = null);
    Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance,int ? excludeId = null);
    
    Task<Entities.Users.User?> AuthenticateAsync(string username,string password);
    
    Task<Entities.Users.User?> GetByUsernameAndPasswordAsync(string username,string password);

    Task<Entities.Users.User?> GetByIdAsyncWithCore(int id);
    
}
