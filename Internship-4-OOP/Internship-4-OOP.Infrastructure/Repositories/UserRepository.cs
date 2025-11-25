using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class UserRepository(DbContext dbContext, DbSet<User> dbSet) : Repository<User,int>(dbContext, dbSet),IUserRepository
{
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await dbSet.AnyAsync(user => user.Email == email);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await dbSet.AnyAsync(user=>user.Username == username);
    }

    public async Task<bool> ExistsUserWithinDistanceAsync(decimal lat, decimal lng, double minDistance)
    {
        return await dbSet.AnyAsync(user => UserDistance.HevrsineDistance(user.GeoLatitude, user.GeoLongitude, lat, lng)<minDistance);

    }


    Task IUserRepository.GetById(int id)
    {
        return GetById(id);
    }
}