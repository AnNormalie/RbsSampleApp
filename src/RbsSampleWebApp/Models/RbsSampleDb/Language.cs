using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RbsSampleWebApp.Models.RbsSampleDb
{
  [Table("Language", Schema = "dbo")]
  public partial class Language
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<Location> Locations { get; set; }
    public string Tag
    {
      get;
      set;
    }
    public string EnglishName
    {
      get;
      set;
    }
  }
}
