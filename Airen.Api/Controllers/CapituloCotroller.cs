using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CapituloController : ControllerBase 
{
    private IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto> _repository;

    public CapituloController(IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaCapitulo([FromBody] CreateCapituloDto capituloDto) 
    {
        var capitulo = capituloDto.MapToCapitulo();

        _repository.Create(capitulo);
        
        return CreatedAtAction(nameof(BuscaCapituloPorId), new {Id = capitulo.Id}, capitulo.MapToReadCapitulo());
    }

    [HttpGet]
    public IActionResult VerCapitulos() 
    {
        var listaCapitulos = _repository.ReadAll();
        return Ok(listaCapitulos);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaCapituloPorId(int id) 
    {
        var capitulo = _repository.ReadById(id);
        if (capitulo == null) return NotFound();
        else return Ok(capitulo);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCapitulo(int id, [FromBody] UpdateCapituloDto capituloDto) 
    {
        var capitulo = _repository.ReadEntityById(id);
        if (capitulo == null) return NotFound();

        var capituloAtualizado = _repository.Update(capituloDto, capitulo);

        return Ok(capituloAtualizado);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaCapitulo (int id) 
    {
        var capitulo = _repository.ReadEntityById(id);
        if (capitulo == null) return NotFound();

        _repository.Delete(capitulo);

        return NoContent();
    }
}