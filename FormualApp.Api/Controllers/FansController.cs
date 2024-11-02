using FormualApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormualApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FansController : ControllerBase
{
    private readonly IFanService _fanService;

    public FansController(IFanService fanService)
    {
        _fanService = fanService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var fans = await _fanService.GetAll();

        return fans.Any() ? Ok(fans) : NotFound();
    }
}