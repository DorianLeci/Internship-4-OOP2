using Internship_4_OOP.Domain.Common.Base;

namespace Internship_4_OOP.Domain.Common.Events.Company;

public class CompanyDeletedEvent(int version, string eventType, int aggregateId, DateTimeOffset timestamp, Entities.Company.Company item) :
    BaseEvent<Entities.Company.Company>(version, eventType, aggregateId, timestamp, item)
{
    
}