using System.ComponentModel.DataAnnotations;

namespace RbsSampleApi.Domain;

public abstract class AuditableBaseEntity
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    public virtual DateTime CreatedOn { get; private set; }
    public virtual string CreatedBy { get; private set; }
    public virtual DateTime? LastModifiedOn { get; private set; }
    public virtual string LastModifiedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public void UpdateCreationProperties(DateTime createdOn, string createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    public void UpdateModifiedProperties(DateTime? lastModifiedOn, string lastModifiedBy)
    {
        LastModifiedOn = lastModifiedOn;
        LastModifiedBy = lastModifiedBy;
    }

    public void UpdateIsDeleted(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }
}
