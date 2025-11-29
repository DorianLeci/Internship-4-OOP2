using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class Repository<TEntity, TId>: IRepository<TEntity, TId>
    where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;
    
    public Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
        
    }

    public async Task<GetAllResponse<TEntity>> GetAsync()
    {
        var entities = await DbSet.ToListAsync();
        return new GetAllResponse<TEntity>(entities);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task<TEntity?> DeleteAsync(TId id)
    {
        var entity= await DbSet.FindAsync(id);
        if (entity == null)
            return null;
        
        DbSet.Remove(entity);
        return entity;
    }

    public void Delete(TEntity? entity)
    {
        if (entity != null)
        {
            DbSet.Remove(entity);
        }
    }
}