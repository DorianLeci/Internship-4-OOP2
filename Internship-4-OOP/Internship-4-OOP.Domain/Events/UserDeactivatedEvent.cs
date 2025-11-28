using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Domain.Events;

public class UserDeactivatedEvent:BaseEvent<User>
{
    public UserDeactivatedEvent(int version,string eventType,int aggregateId,DateTimeOffset timestamp,User user)
        : base(version,eventType, aggregateId, timestamp, user)

    { }
}