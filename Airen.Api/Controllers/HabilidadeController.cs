using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Api.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HabilidadeController : ControllerBase {

    private AirenContext _context;
    private HabilidadeServices _services;

    public HabilidadeController(AirenContext context, HabilidadeServices services) {
        _context = context;
        _services = services;
    }

    [HttpPost]
    public IActionResult AdicionarHabilidade([FromBody] CreateHabilidadeDto habilidadeDto) {
        var habilidade = habilidadeDto.MapToHabilidade();
        _context.Habilidades!.Add(habilidade);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaHabilidadePorId), new {Id = habilidade.Id}, habilidade);
    }

    [HttpGet]
    public IEnumerable<ReadHabilidadeDto> VerHabilidades() {
        var listaHabilidades = _services.GetAll();
        return listaHabilidades;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHabilidadePorId(int id) {
        var habilidade = _services.GetById(id);
        if (habilidade == null) return NotFound();
        else return Ok(habilidade.MapToReadHabilidade());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHabilidade(int id, [FromBody] UpdateHabilidadeDto habilidadeDto) {
        var habilidade = _services.GetById(id);
        if (habilidade == null) return NotFound();

        habilidadeDto.MapToHabilidade(habilidade);
        _context.SaveChanges();

        return Ok(habilidadeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHabilidade (int id) {
        var habilidade = _services.GetById(id);
        if (habilidade == null) return NotFound();

        _context.Habilidades!.Remove(habilidade);
        _context.SaveChanges();

        return NoContent();
    }
}