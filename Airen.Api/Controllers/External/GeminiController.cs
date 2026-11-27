using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers.External;

[ApiController]
[Route("[controller]")]
public class GeminiController : ControllerBase 
{
    private readonly IGeminiService _geminiService;

    public GeminiController(IGeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    [HttpPost]
    public async Task<IActionResult> CorrigirOrtografia([FromBody] string texto, CancellationToken cancellationToken)
    {
        var response = await _geminiService.CorrigirOrtografiaAsync(texto, cancellationToken);
        return Ok(response);
    }
}