using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RbsSampleWebApp.Models.RbsSampleDb
{
  [Table("Location", Schema = "dbo")]
  public partial class Location
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }
    public string Key
    {
      get;
      set;
    }
    public Guid LanguageId
    {
      get;
      set;
    }
    public Language Language { get; set; }

    [Column(TypeName="xml")]
    public string Information
    {
      get;
      set;
    }
    public bool Exist
    {
      get;
      set;
    }
    public DateTime CreatedOn
    {
      get;
      set;
    }
    public string CreatedBy
    {
      get;
      set;
    }
    public DateTime? LastModifiedOn
    {
      get;
      set;
    }
    public string LastModifiedBy
    {
      get;
      set;
    }
    public bool IsDeleted
    {
      get;
      set;
    }
  }
}
