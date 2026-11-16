using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnicaController : ControllerBase 
{
    private IRepository<Tecnica, ReadTecnicaDto, UpdateTecnicaDto> _repository;

    public TecnicaController(IRepository<Tecnica, ReadTecnicaDto, UpdateTecnicaDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaTecnica([FromBody] CreateTecnicaDto tecnicaDto) 
    {
        var tecnica = tecnicaDto.MapToTecnica();
        _repository.Create(tecnica);
        return CreatedAtAction(nameof(BuscaTecnicaPorId), new {Id = tecnica.Id}, tecnica.MapToReadTecnica());
    }

    [HttpGet]
    public IActionResult VerTecnicas() 
    {
        var listaTecnicas = _repository.ReadAll();
        return Ok(listaTecnicas);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaTecnicaPorId(int id) 
    {
        var tecnica = _repository.ReadById(id);
        if (tecnica == null) return NotFound();
        else return Ok(tecnica);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTecnica(int id, [FromBody] UpdateTecnicaDto tecnicaDto) 
    {
        var tecnica = _repository.ReadEntityById(id);
        if (tecnica == null) return NotFound();

        _repository.Update(tecnicaDto, tecnica);

        return Ok(tecnicaDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaTecnica (int id) 
    {
        var tecnica = _repository.ReadEntityById(id);
        if (tecnica == null) return NotFound();

        _repository.Delete(tecnica);

        return NoContent();
    }
}