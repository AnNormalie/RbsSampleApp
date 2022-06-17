namespace RbsSampleApi.Domain.Languages.Mappings;

using AutoMapper;
using RbsSampleApi.Domain.Languages;
using RBSSample.Shared.Dtos.Language;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        //createmap<to this, from this>
        CreateMap<Language, LanguageDto>()
            .ForMember(m => m.Locations, opt => opt.ExplicitExpansion())
            .ReverseMap();
        CreateMap<LanguageForCreationDto, Language>();
        CreateMap<LanguageForUpdateDto, Language>()
            .ReverseMap();
    }
}