namespace RbsSampleApi.Domain.Languages;


using RbsSampleApi.Domain.Languages.Mappings;
using RbsSampleApi.Domain.Languages.Validators;
using AutoMapper;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using RbsSampleApi.Domain.Locations;
using RBSSample.Shared.Dtos.Language;

[Table("Language", Schema="dbo")]
public class Language : BaseEntity
{
    [Required]
    [StringLength(5)]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Tag { get; private set; }

    [Required]
    [StringLength(50)]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string EnglishName { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Location> Locations { get; private set; }


    public static Language Create(LanguageForCreationDto languageForCreationDto)
    {
        new LanguageForCreationDtoValidator().ValidateAndThrow(languageForCreationDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<LanguageProfile>();
        }));
        var newLanguage = mapper.Map<Language>(languageForCreationDto);
        
        return newLanguage;
    }

    public void Update(LanguageForUpdateDto languageForUpdateDto)
    {
        new LanguageForUpdateDtoValidator().ValidateAndThrow(languageForUpdateDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<LanguageProfile>();
        }));
        mapper.Map(languageForUpdateDto, this);
    }
    
    protected Language() { } // For EF + Mocking
}