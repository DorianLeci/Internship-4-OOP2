namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserRepository
{
    Task<User> GetById(int id);
    Task<bool>ExistsByEmailAsync(string email);
    Task<bool>ExistsByUsernameAsync(string username);
    Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance);
    
}
