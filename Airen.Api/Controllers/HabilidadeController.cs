using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Airen.Api.Repositories;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HabilidadeController : ControllerBase {

    private AirenContext _context;
    private IRepository<Habilidade, ReadHabilidadeDto> _repository;

    public HabilidadeController(AirenContext context, IRepository<Habilidade, ReadHabilidadeDto> repository) {
        _context = context;
        _repository = repository;
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
        var listaHabilidades = _repository.GetAll();
        return listaHabilidades;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHabilidadePorId(int id) {
        var habilidade = _repository.GetById(id);
        if (habilidade == null) return NotFound();
        else return Ok(habilidade.MapToReadHabilidade());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHabilidade(int id, [FromBody] UpdateHabilidadeDto habilidadeDto) {
        var habilidade = _repository.GetById(id);
        if (habilidade == null) return NotFound();

        habilidadeDto.MapToHabilidade(habilidade);
        _context.SaveChanges();

        return Ok(habilidadeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHabilidade (int id) {
        var habilidade = _repository.GetById(id);
        if (habilidade == null) return NotFound();

        _context.Habilidades!.Remove(habilidade);
        _context.SaveChanges();

        return NoContent();
    }
}