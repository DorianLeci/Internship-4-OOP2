using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Common;
using Internship_4_OOP.Infrastructure.Database.Configuration.Users;
using Internship_4_OOP.Infrastructure.Manager;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class UserRepository(UserDbContext context,IDapperManager<User> dapperManager) : Repository<User, int>(context,dapperManager), IUserRepository
{
    
    public async Task<User?> GetByIdAsync(int id)
    {
        const string sql = "SELECT* FROM Users WHERE id = @Id";
        
        return await DapperManager.QuerySingleAsync(sql,new { Id = id });
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        const string sql = "SELECT* FROM Users";
        return await DapperManager.QueryAsync(sql);
    }

    public async Task<User?> GetByUsernameAndPasswordAsync(string username,string password)
    {
        const string sql = "SELECT* FROM Users WHERE username = @username AND password = @password";

        return await DapperManager.QuerySingleAsync(sql,new { username,password });
    }
    

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await DbSet.AnyAsync(user => user.Email == email);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await DbSet.AnyAsync(user=>user.Username == username);
    }

    public async Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance)
    {
        var users=await DbSet.ToListAsync();
        return users.Any(user => UserDistance.HevrsineDistance(user.GeoLatitude, user.GeoLongitude, lat, lng)<minDistance);

    }

    public async Task<User?> AuthenticateAsync(string username,string password)
    {
        var user=await GetByUsernameAndPasswordAsync(username,password);
            
        return user;
    }
    
}