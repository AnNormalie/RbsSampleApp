using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RbsSampleWebApp.Models.RbsSampleDb;

namespace RbsSampleWebApp.Data
{
  public partial class RbsSampleDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public RbsSampleDbContext(DbContextOptions<RbsSampleDbContext> options):base(options)
    {
    }

    public RbsSampleDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RbsSampleWebApp.Models.RbsSampleDb.Location>()
              .HasOne(i => i.Language)
              .WithMany(i => i.Locations)
              .HasForeignKey(i => i.LanguageId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<RbsSampleWebApp.Models.RbsSampleDb.Location>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<RbsSampleWebApp.Models.RbsSampleDb.Location>()
              .Property(p => p.LastModifiedOn)
              .HasColumnType("datetime2");
        this.OnModelBuilding(builder);
    }


    public DbSet<RbsSampleWebApp.Models.RbsSampleDb.Language> Languages
    {
      get;
      set;
    }

    public DbSet<RbsSampleWebApp.Models.RbsSampleDb.Location> Locations
    {
      get;
      set;
    }
  }
}
