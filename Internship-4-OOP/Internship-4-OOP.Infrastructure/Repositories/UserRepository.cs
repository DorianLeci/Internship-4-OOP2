using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Common;
using Internship_4_OOP.Infrastructure.Database.Configuration.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class UserRepository(UserDbContext context) : Repository<User, int>(context), IUserRepository
{
    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
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
        return await DbSet.AnyAsync(user => UserDistance.HevrsineDistance(user.GeoLatitude, user.GeoLongitude, lat, lng)<minDistance);

    }
    
}