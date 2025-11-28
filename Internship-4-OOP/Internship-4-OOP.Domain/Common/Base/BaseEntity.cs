namespace Internship_4_OOP.Domain.Common.Base;

public abstract class BaseEntity<T>(int id, string name):IAuditableEntity
{
    public int Id { get; init; } = id;
    public DateTime CreatedAt{get; protected set; }
    public DateTime UpdatedAt{get; protected set; }
    public DateTime? DeletedAt { get; protected set; } = null;
    
    public void SetCreatedAt()
    {
        CreatedAt=DateTime.Now;
    }

    public void SetUpdatedAt()
    { 
        UpdatedAt=DateTime.Now;
    }

    public void SetDeletedAt()
    {
        DeletedAt=DateTime.Now;
    }

    public string Name{get; set;} = name;

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