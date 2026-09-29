using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Airen.Api.Repositories;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HabilidadeController : ControllerBase 
{
    private IRepository<Habilidade, ReadHabilidadeDto, UpdateHabilidadeDto> _repository;

    public HabilidadeController(IRepository<Habilidade, ReadHabilidadeDto, UpdateHabilidadeDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionarHabilidade([FromBody] CreateHabilidadeDto habilidadeDto) 
    {
        var habilidade = habilidadeDto.MapToHabilidade();
        _repository.Create(habilidade);
        return CreatedAtAction(nameof(BuscaHabilidadePorId), new {Id = habilidade.Id}, habilidade);
    }

    [HttpGet]
    public IEnumerable<ReadHabilidadeDto> VerHabilidades() 
    {
        var listaHabilidades = _repository.ReadAll();
        return listaHabilidades;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHabilidadePorId(int id) 
    {
        var habilidade = _repository.ReadById(id);
        if (habilidade == null) return NotFound();
        else return Ok(habilidade.MapToReadHabilidade());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHabilidade(int id, [FromBody] UpdateHabilidadeDto habilidadeDto) 
    {
        var habilidade = _repository.ReadById(id);
        if (habilidade == null) return NotFound();

        _repository.Update(habilidadeDto, habilidade);

        return Ok(habilidadeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHabilidade (int id) 
    {
        var habilidade = _repository.ReadById(id);
        if (habilidade == null) return NotFound();

        _repository.Delete(habilidade);

        return NoContent();
    }
}