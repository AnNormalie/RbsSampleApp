using Microsoft.Extensions.Configuration;
using RBSSample.Shared.Dtos.Language;
using RBSSample.Shared.Dtos.Location;
using RbsSampleWebApp.Services.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace RbsSampleWebApp.Services;

public class RbsSampleApiService : IRbsSampleApiService
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;
    private readonly JsonSerializerOptions _options;
    public RbsSampleApiService(IConfiguration configuration)
    {
        this.httpClient = new HttpClient();
        this.baseUri = new Uri(configuration["RbsRestApiBaseUri"]);
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<LanguageDto> CreateLanguage(LanguageForCreationDto body)
    {
        var uri = new Uri(baseUri, $"api/Languages");

        var message = new HttpRequestMessage(HttpMethod.Post, uri);

        message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<LanguageDto>(content, _options);

    }
    public async Task<LocationDto> CreateLocation(LocationForCreationDto body)
    {
        var uri = new Uri(baseUri, $"api/Locations");

        var message = new HttpRequestMessage(HttpMethod.Post, uri);

        message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<LocationDto>(content, _options);

    }

    public async Task<bool> DeleteLanguage(Guid id)
    {
        var uri = new Uri(baseUri, $"api/Language/{id}");

        var message = new HttpRequestMessage(HttpMethod.Delete, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        return true;
    }
    public async Task<bool> DeleteLocation(Guid id)
    {
        var uri = new Uri(baseUri, $"api/Locations/{id}");

        var message = new HttpRequestMessage(HttpMethod.Delete, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        return true;
    }

    public async Task<PagedLanguageResultDto> GetLanguagesPaged(LanguageParametersDto queryParameters)
    {
        var uri = new Uri(baseUri, $"api/Languages");

        var uriBuilder = new UriBuilder(uri);
        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (queryParameters.Filters != null)
            queryString.Add("Filters", $"{queryParameters.Filters}");
        if (queryParameters.SortOrder != null)
            queryString.Add("SortOrder", $"{queryParameters.SortOrder}");
        if (queryParameters.Includes != null)
            queryString.Add("Includes", $"{string.Join(",", queryParameters.Includes)}");
        queryString.Add("PageNumber", $"{queryParameters.PageNumber}");
        queryString.Add("PageSize", $"{queryParameters.PageSize}");

        uriBuilder.Query = queryString.ToString();
        uri = uriBuilder.Uri;

        var message = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<PagedLanguageResultDto>(content, _options);

    }
    public async Task<PagedLocationResultDto> GetLocationsPaged(LocationParametersDto queryParameters)
    {
        var uri = new Uri(baseUri, $"api/Locations");

        var uriBuilder = new UriBuilder(uri);
        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (queryParameters.Filters != null)
            queryString.Add("Filters", $"{queryParameters.Filters}");
        if (queryParameters.SortOrder != null)
            queryString.Add("SortOrder", $"{queryParameters.SortOrder}");
        if (queryParameters.Includes != null)
            queryString.Add("Includes", $"{string.Join(",", queryParameters.Includes)}");
        queryString.Add("PageNumber", $"{queryParameters.PageNumber}");
        queryString.Add("PageSize", $"{queryParameters.PageSize}");

        uriBuilder.Query = queryString.ToString();
        uri = uriBuilder.Uri;

        var message = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<PagedLocationResultDto>(content, _options);
    }

    public async Task<LanguageDto> GetLanguageById(Guid id, string[] includes = null)
    {
        var uri = new Uri(baseUri, $"api/Languages/{id}");

        var uriBuilder = new UriBuilder(uri);
        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (includes != null)
            queryString.Add("Includes", $"{string.Join(",", includes)}");

        uriBuilder.Query = queryString.ToString();
        uri = uriBuilder.Uri;

        var message = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<LanguageDto>(content, _options);
    }
    public async Task<LocationDto> GetLocationById(Guid id, string[] includes = null)
    {
        var uri = new Uri(baseUri, $"api/Locations/{id}");

        var uriBuilder = new UriBuilder(uri);
        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (includes != null)
            queryString.Add("Includes", $"{string.Join(",", includes)}");

        uriBuilder.Query = queryString.ToString();
        uri = uriBuilder.Uri;

        var message = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<LocationDto>(content, _options);
    }

    public async Task<bool> UpdateLanguage(Guid id, LanguageForUpdateDto body)
    {
        var uri = new Uri(baseUri, $"api/Languages/{id}");

        var message = new HttpRequestMessage(HttpMethod.Put, uri);

        message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        return true;
    }
    public async Task<bool> UpdateLocation(Guid id, LocationForUpdateDto body)
    {
        var uri = new Uri(baseUri, $"api/Locations/{id}");

        var message = new HttpRequestMessage(HttpMethod.Put, uri);

        message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        return true;
    }
}
