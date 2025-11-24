namespace Internship_4_OOP.Domain.Common.Model;

public class GetAllResponse<TEntity>(List<TEntity> values)
{
    public IEnumerable<TEntity> Values { get; init; } = values.AsEnumerable();
}