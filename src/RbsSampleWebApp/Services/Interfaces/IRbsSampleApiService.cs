using RBSSample.Shared.Dtos.Language;
using RBSSample.Shared.Dtos.Location;
using System;
using System.Threading.Tasks;

namespace RbsSampleWebApp.Services.Interfaces;

public interface IRbsSampleApiService
{
    #region Language
    Task<PagedLanguageResultDto> GetLanguagesPaged(LanguageParametersDto queryParameters);
    Task<LanguageDto> GetLanguageById(Guid id, string[] includes = null);
    Task<LanguageDto> CreateLanguage(LanguageForCreationDto body);
    Task<bool> UpdateLanguage(Guid id, LanguageForUpdateDto body);
    Task<bool> DeleteLanguage(Guid id);
    #endregion Language


    #region Location
    Task<PagedLocationResultDto> GetLocationsPaged(LocationParametersDto queryParameters);
    Task<LocationDto> GetLocationById(Guid id, string[] includes = null);
    Task<LocationDto> CreateLocation(LocationForCreationDto body);
    Task<bool> UpdateLocation(Guid id, LocationForUpdateDto body);
    Task<bool> DeleteLocation(Guid id);
    #endregion Location    
}