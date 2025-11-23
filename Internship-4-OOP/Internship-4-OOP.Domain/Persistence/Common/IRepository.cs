using Internship_4_OOP.Domain.Common.Model;

namespace Internship_4_OOP.Domain.Persistence.Common;

public interface IRepository<TEntity,TId> where TEntity : class
{
    Task<GetAllResponse<TEntity>> GetAsync();
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);  
    Task DeleteAsync(TId id);
    void Delete(TEntity? entity);
}