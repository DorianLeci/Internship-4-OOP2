using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class Repository<TEntity, TId>(DbContext dbContext,DbSet<TEntity> dbSet) : IRepository<TEntity, TId>
    where TEntity : class
{
    private readonly DbContext _dbContext = dbContext;

    public async Task<GetAllResponse<TEntity>> GetAsync()
    {
        var entities = await dbSet.ToListAsync();
        return new GetAllResponse<TEntity>(entities);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }

    public async Task DeleteAsync(TId id)
    {
        var entity= await dbSet.FindAsync(id);
        if(entity != null){
            dbSet.Remove(entity);
        }
    }

    public void Delete(TEntity? entity)
    {
        if (entity != null)
        {
            dbSet.Remove(entity);
        }
    }
}