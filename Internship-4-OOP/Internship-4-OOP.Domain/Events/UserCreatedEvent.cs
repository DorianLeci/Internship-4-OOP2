using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Domain.Events;

public class UserCreatedEvent: BaseEvent<User>
{
    public UserCreatedEvent(int version,string eventType,int aggregateId,DateTimeOffset timestamp,User user)
        : base(version,eventType, aggregateId, timestamp, user)

    { }
}