using FormualApp.Api.Domains;

namespace FormualApp.Api.Services.Interfaces;

public interface IFanService
{
    Task<List<Fan>?> GetAll();
}