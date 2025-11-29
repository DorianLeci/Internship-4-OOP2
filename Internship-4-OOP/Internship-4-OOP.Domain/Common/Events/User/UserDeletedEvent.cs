using Internship_4_OOP.Domain.Common.Base;

namespace Internship_4_OOP.Domain.Common.Events.User;

public class UserDeletedEvent(
    int version,
    string eventType,
    int aggregateId,
    DateTimeOffset timestamp,
    Entities.Users.User user)
    : BaseEvent<Entities.Users.User>(version, eventType, aggregateId, timestamp, user);