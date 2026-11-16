using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParentescoController : ControllerBase 
{
    private IRepository<Parentesco, ReadParentescoDto, UpdateParentescoDto> _repository;

    public ParentescoController(IRepository<Parentesco, ReadParentescoDto, UpdateParentescoDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionarParentesco([FromBody] CreateParentescoDto parentescoDto) 
    {
        var parentesco = parentescoDto.MapToParentesco();
        _repository.Create(parentesco);
        return CreatedAtAction(nameof(BuscaParentescoPorId), new {Id = parentesco.Id}, parentesco.MapToReadParentesco());
    }

    [HttpGet]
    public IActionResult VerParentescos() 
    {
        var listaParentescos = _repository.ReadAll();
        return Ok(listaParentescos);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaParentescoPorId(int id) 
    {
        var parentesco = _repository.ReadById(id);
        if (parentesco == null) return NotFound();
        else return Ok(parentesco);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaParentesco(int id, [FromBody] UpdateParentescoDto parentescoDto) 
    {
        var parentesco = _repository.ReadEntityById(id);
        if (parentesco == null) return NotFound();

        _repository.Update(parentescoDto, parentesco);

        return Ok(parentescoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaParentesco (int id) 
    {
        var parentesco = _repository.ReadEntityById(id);
        if (parentesco == null) return NotFound();

        _repository.Delete(parentesco);

        return NoContent();
    }
}