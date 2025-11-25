using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;
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

    Task IUserRepository.GetById(int id)
    {
        return GetById(id);
    }
}