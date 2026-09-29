using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParentescoController : ControllerBase {

    private AirenContext _context;
    private IRepository<Parentesco, ReadParentescoDto> _repository;

    public ParentescoController(AirenContext context, IRepository<Parentesco, ReadParentescoDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionarParentesco([FromBody] CreateParentescoDto parentescoDto) {
        var parentesco = parentescoDto.MapToParentesco();
        _context.Parentescos!.Add(parentesco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaParentescoPorId), new {Id = parentesco.Id}, parentesco);
    }

    [HttpGet]
    public IEnumerable<ReadParentescoDto> VerParentescos() {
        var listaParentescos = _repository.GetAll();
        return listaParentescos;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaParentescoPorId(int id) {
        var parentesco = _repository.GetById(id);
        if (parentesco == null) return NotFound();
        else return Ok(parentesco.MapToReadParentesco());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaParentesco(int id, [FromBody] UpdateParentescoDto parentescoDto) {
        var parentesco = _repository.GetById(id);
        if (parentesco == null) return NotFound();

        parentescoDto.MapToParentesco(parentesco);
        _context.SaveChanges();

        return Ok(parentescoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaParentesco (int id) {
        var parentesco = _repository.GetById(id);
        if (parentesco == null) return NotFound();

        _context.Parentescos!.Remove(parentesco);
        _context.SaveChanges();

        return NoContent();
    }
}