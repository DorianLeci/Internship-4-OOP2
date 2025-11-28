using Internship_4_OOP.Domain.Common.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Events;

public class DomainEventLogger<T>(ILogger<T> logger) : INotificationHandler<BaseEvent<T>>
{
    public Task Handle(BaseEvent<T> notification, CancellationToken cancellationToken)
    {
       logger.LogInformation("Domain event: {EventName} {@DomainEvent}",notification.GetType().Name,notification);
       return Task.CompletedTask;
    }
}