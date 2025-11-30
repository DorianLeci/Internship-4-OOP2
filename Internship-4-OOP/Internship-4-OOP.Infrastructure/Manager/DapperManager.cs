using Dapper;
using Npgsql;

namespace Internship_4_OOP.Infrastructure.Manager;

public class DapperManager<TEntity>(string connectionString) : IDapperManager<TEntity> where TEntity : class
{
    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
    public async Task<IReadOnlyList<TEntity>> QueryAsync(string sql, object? param = null) 
    { 
        using var connection = CreateConnection();
        await connection.OpenAsync();
        
        var result= await connection.QueryAsync<TEntity>(sql, param);
        return result.ToList();
    }

    public async Task<TEntity?> QuerySingleAsync(string sql, object? param = null)
    {
        using var connection = CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sql, param);
        return result;
    }

    public Task ExecuteAsync(string sql, object? param = null)
    {
        throw new NotImplementedException();
    }
}