namespace Internship_4_OOP.Domain.Common.Base;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    DateTime? DeletedAt { get; }
    
    void SetCreatedAt();
    void SetUpdatedAt();
    void SetDeletedAt();
    
}