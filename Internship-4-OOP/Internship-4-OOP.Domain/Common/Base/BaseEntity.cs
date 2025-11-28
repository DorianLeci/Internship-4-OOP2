namespace Internship_4_OOP.Domain.Common.Base;

public abstract class BaseEntity<T>
{
    public int Id { get; init; }
    public DateTime CreatedAt{get; init; }
    public DateTime UpdatedAt{get; protected set; }
    public DateTime? DeletedAt { get; init; } = null;
    public string Name{get; set;}

    protected BaseEntity(int id,string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;        
    }

    private readonly List<BaseEvent<T>> _domainEvents = [];
    
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