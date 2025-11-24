using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class UserRepository(DbContext dbContext, DbSet<User> dbSet) : Repository<User,int>(dbContext, dbSet),IUserRepository
{
    public Task GetById(int id)
    {
        throw new NotImplementedException();
    }
}