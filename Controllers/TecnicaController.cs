using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Profiles;
using AirenApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnicaController : ControllerBase {

    private AirenContext _context;
    private TecnicaService _service;

    public TecnicaController(AirenContext context, TecnicaService service) {
        _context = context;
        _service = service;
    }

    [HttpPost]
    public IActionResult AdicionaTecnica([FromBody] CreateTecnicaDto tecnicaDto) {
        var tecnica = tecnicaDto.MapToTecnica();
        _context.Tecnicas!.Add(tecnica);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaTecnicaPorId), new {Id = tecnica.Id}, tecnica);
    }

    [HttpGet]
    public IEnumerable<ReadTecnicaDto> VerTecnicas() {
        var listaTecnicas = _service.GetAll();
        return listaTecnicas;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaTecnicaPorId(int id) {
        var tecnica = _service.GetById(id);
        if (tecnica == null) return NotFound();
        else return Ok(tecnica.MapToReadTecnica());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTecnica(int id, [FromBody] UpdateTecnicaDto tecnicaDto) {
        var tecnica = _service.GetById(id);
        if (tecnica == null) return NotFound();

        tecnicaDto.MapToTecnica(tecnica);
        _context.SaveChanges();

        return Ok(tecnicaDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaTecnica (int id) {
        var tecnica = _service.GetById(id);
        if (tecnica == null) return NotFound();

        _context.Tecnicas!.Remove(tecnica);
        _context.SaveChanges();

        return NoContent();
    }
}