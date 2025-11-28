namespace Internship_4_OOP.Domain.Common.Events;

public interface IDomainEvent
{
    int Version { get; }
    Guid EventId { get; }
    string EventType { get; }
    int AggregateId { get; }
    DateTimeOffset Timestamp { get; }
}