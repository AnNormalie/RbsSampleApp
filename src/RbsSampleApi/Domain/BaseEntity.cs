namespace RbsSampleApi.Domain;

using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class BaseEntity
{
    [Key]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid Id { get; private set; } = Guid.NewGuid();
    
    



    
}