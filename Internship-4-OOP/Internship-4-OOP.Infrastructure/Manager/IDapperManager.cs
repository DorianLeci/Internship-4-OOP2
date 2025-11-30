namespace Internship_4_OOP.Infrastructure.Manager;

public interface IDapperManager<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> QueryAsync(string sql, object? param = null);
    
    Task<TEntity?> QuerySingleAsync(string sql, object? param = null);
    Task ExecuteAsync(string sql, object? param = null);
}