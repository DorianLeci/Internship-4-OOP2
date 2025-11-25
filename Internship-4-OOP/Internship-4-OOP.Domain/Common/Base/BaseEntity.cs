namespace Internship_4_OOP.Domain.Common.Base;

public abstract class BaseEntity
{
    public int Id { get; init; }
    public DateTime CreatedAt{get; init; }
    public DateTime UpdatedAt{get; protected set; }
    public DateTime? DeletedAt { get; init; } = null;
    public string Name{get; set;}

    protected BaseEntity(string name)
    {
        Name = name;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;        
    }

    private readonly List<BaseEvent> _domainEvents = [];
    
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
}