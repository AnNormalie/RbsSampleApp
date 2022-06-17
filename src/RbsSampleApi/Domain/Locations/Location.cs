namespace RbsSampleApi.Domain.Locations;

using RBSSample.Shared.Dtos.Location;
using RbsSampleApi.Domain.Locations.Mappings;
using RbsSampleApi.Domain.Locations.Validators;
using AutoMapper;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using RbsSampleApi.Domain.Languages;

[Table("Location", Schema="dbo")]
public class Location : AuditableBaseEntity
{
    [Required]
    [StringLength(50)]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Key { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Language")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid LanguageId { get; private set; }
    public virtual Language Language { get; private set; }

    [Column(TypeName = "xml")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Information { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool Exist { get; private set; }


    public static Location Create(LocationForCreationDto locationForCreationDto)
    {
        new LocationForCreationDtoValidator().ValidateAndThrow(locationForCreationDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<LocationProfile>();
        }));
        var newLocation = mapper.Map<Location>(locationForCreationDto);
        
        return newLocation;
    }

    public void Update(LocationForUpdateDto locationForUpdateDto)
    {
        new LocationForUpdateDtoValidator().ValidateAndThrow(locationForUpdateDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<LocationProfile>();
        }));
        mapper.Map(locationForUpdateDto, this);
    }
    
    protected Location() { } // For EF + Mocking
}