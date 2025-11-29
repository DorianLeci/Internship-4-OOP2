using System.ComponentModel.DataAnnotations.Schema;

namespace Internship_4_OOP.Domain.Common.Base;

public abstract class BaseEntity<T> :IAuditableEntity
{
    public int Id { get; init; }
    public string Name{get; set;}

    private readonly List<BaseEvent<T>> _domainEvents = new List<BaseEvent<T>>();

    public BaseEntity()
    { }
    protected BaseEntity(string name)
    {
        Name = name;
    }

    public DateTime CreatedAt{get; set; }
    
    public DateTime UpdatedAt{get; set; }
    
    [NotMapped]
    public DateTime? DeletedAt { get; set; } = null;
    
    
    [NotMapped]
    public IReadOnlyCollection<BaseEvent<T>> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent<T> domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent<T> domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
}