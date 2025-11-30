using Internship_4_OOP.Domain.Common.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Events;

public class DomainEventLogger<TEvent>(ILogger<TEvent> logger) : INotificationHandler<TEvent> where TEvent:INotification,IDomainEvent
{
    public Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Domain event {EventId} Version: {Version} Event Type: {EventType}, AggregateId: {AggregateId}, Timestamp: {Timestamp}",
            notification.EventId,
            notification.Version,
            notification.EventType,
            notification.AggregateId,
            notification.Timestamp
        );

        return Task.CompletedTask;
    }
}