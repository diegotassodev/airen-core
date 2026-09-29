using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnicaController : ControllerBase {

    private AirenContext _context;
    private IRepository<Tecnica, ReadTecnicaDto> _repository;

    public TecnicaController(AirenContext context, IRepository<Tecnica, ReadTecnicaDto> repository) {
        _context = context;
        _repository = repository;
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
        var listaTecnicas = _repository.GetAll();
        return listaTecnicas;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaTecnicaPorId(int id) {
        var tecnica = _repository.GetById(id);
        if (tecnica == null) return NotFound();
        else return Ok(tecnica.MapToReadTecnica());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTecnica(int id, [FromBody] UpdateTecnicaDto tecnicaDto) {
        var tecnica = _repository.GetById(id);
        if (tecnica == null) return NotFound();

        tecnicaDto.MapToTecnica(tecnica);
        _context.SaveChanges();

        return Ok(tecnicaDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaTecnica (int id) {
        var tecnica = _repository.GetById(id);
        if (tecnica == null) return NotFound();

        _context.Tecnicas!.Remove(tecnica);
        _context.SaveChanges();

        return NoContent();
    }
}