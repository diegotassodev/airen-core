using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers.External;

[ApiController]
[Route("[controller]")]
public class GeminiController : ControllerBase 
{
    private readonly GeminiService _geminiService;

    public GeminiController(GeminiService geminiService) 
    {
        _geminiService = geminiService;
    }

    [HttpPost]
    public async Task<IActionResult> RetornarResposta([FromBody] string pergunta) 
    {
        var response = await _geminiService.RetornarResposta(pergunta);
        return Ok(response);
    }
}