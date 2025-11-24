namespace Internship_4_OOP.Infrastructure.Manager;

public interface IDapperManager
{
    Task<IReadOnlyList<TEntity>> QueryAsync<TEntity>(string sql,object ?param=null) where TEntity : class;
    Task<TEntity?> QuerySingleAsync<TEntity>(string sql, object? param = null);
    Task ExecuteAsync(string sql, object? param = null);
}