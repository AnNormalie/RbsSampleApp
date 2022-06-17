using RBSSample.Shared.Dtos.Language;
using RBSSample.Shared.Dtos.Location;
using System.Threading.Tasks;

namespace RbsSampleWebApp.Services.Interfaces;

public interface IRbsSampleApiService
{
    #region Language
    Task<PagedLanguageResultDto> GetLanguagesPaged(LanguageParametersDto queryParameters);
    Task<LanguageDto> GetLanguageById(int id, string[] includes = null);
    Task<LanguageDto> CreateLanguage(LanguageForCreationDto body);
    Task<bool> UpdateLanguage(int id, LanguageForUpdateDto body);
    Task<bool> DeleteLanguage(int id);
    #endregion Language


    #region Location
    Task<PagedLocationResultDto> GetLocationsPaged(LocationParametersDto queryParameters);
    Task<LocationDto> GetLocationById(int id, string[] includes = null);
    Task<LocationDto> CreateLocation(LocationForCreationDto body);
    Task<bool> UpdateLocation(int id, LocationForUpdateDto body);
    Task<bool> DeleteLocation(int id);
    #endregion Location    
}