using Dapper;
using Npgsql;

namespace Internship_4_OOP.Infrastructure.Manager;

internal sealed class DapperManager(string connectionString) : IDapperManager
{
    private readonly string _connectionString = connectionString;

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
    public async Task<IReadOnlyList<TEntity>> QueryAsync<TEntity>(string sql, object? param = null) where TEntity : class
    { 
        using var connection = CreateConnection();
        await connection.OpenAsync();
        
        var result= await connection.QueryAsync<TEntity>(sql, param);
        return result.ToList();
    }

    public async Task<TEntity?> QuerySingleAsync<TEntity>(string sql, object? param = null)
    {
        using var connection = CreateConnection();
        await connection.OpenAsync();

        var result = await connection.QuerySingleAsync<TEntity>(sql, param);
        return result;
    }

    public Task ExecuteAsync(string sql, object? param = null)
    {
        throw new NotImplementedException();
    }
}