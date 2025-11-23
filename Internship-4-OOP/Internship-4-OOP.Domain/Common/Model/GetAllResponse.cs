namespace Internship_4_OOP.Domain.Common.Model;

public class GetAllResponse<TEntity>
{
    public IEnumerable<TEntity> Values { get; init; }
}