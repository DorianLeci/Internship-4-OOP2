using Internship_4_OOP.Domain.Common.Events;

namespace Internship_4_OOP.Domain.Common.Base;
using MediatR;
public abstract class BaseEvent<T>(
    int version,
    string eventType,
    int aggregateId,
    DateTimeOffset timestamp,
    T item)
    : IDomainEvent, INotification
{
    public int Version { get; } = version;

    public Guid EventId { get; }=Guid.NewGuid();

    public string EventType { get; } = eventType;

    public int AggregateId { get; } = aggregateId;

    public DateTimeOffset Timestamp { get; } = timestamp;

    public T Item { get; } = item;
}