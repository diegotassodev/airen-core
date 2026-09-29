using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CapituloController : ControllerBase {

    private AirenContext _context;
    private IRepository<Capitulo, ReadCapituloDto> _repository;

    public CapituloController(AirenContext context, IRepository<Capitulo, ReadCapituloDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaCapitulo([FromBody] CreateCapituloDto capituloDto) {
        var capitulo = capituloDto.MapToCapitulo();
        _context.Capitulos!.Add(capitulo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaCapituloPorId), new {Id = capitulo.Id}, capitulo);
    }

    [HttpGet]
    public IEnumerable<ReadCapituloDto> VerCapitulos() {
        var listaCapitulos = _repository.GetAll();
        return listaCapitulos;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaCapituloPorId(int id) {
        var capitulo = _repository.GetById(id);
        if (capitulo == null) return NotFound();
        else return Ok(capitulo.MapToReadCapitulo());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCapitulo(int id, [FromBody] UpdateCapituloDto capituloDto) {
        var capitulo = _repository.GetById(id);
        if (capitulo == null) return NotFound();

        capituloDto.MapToCapitulo(capitulo);
        _context.SaveChanges();

        return Ok(capituloDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaCapitulo (int id) {
        var capitulo = _repository.GetById(id);
        if (capitulo == null) return NotFound();

        _context.Capitulos!.Remove(capitulo);
        _context.SaveChanges();

        return NoContent();
    }
}