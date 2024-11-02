using System.Net;
using FormualApp.Api.Configuration;
using FormualApp.Api.Domains;
using FormualApp.Api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FormualApp.Api.Services;

public class FanService:IFanService
{
    private readonly HttpClient _httpClient;
    private readonly ApiServiceConfig _options;

    public FanService( HttpClient httpClient  , IOptions<ApiServiceConfig> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }
    public async Task<List<Fan>?> GetAll()
    {
        var response = await _httpClient.GetAsync(_options.Url);

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => new List<Fan>(),
            HttpStatusCode.Unauthorized => null,
            _ => await response.Content.ReadFromJsonAsync<List<Fan>>()
        };
    }
}