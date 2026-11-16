using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PotentiaController : ControllerBase 
{
    private IRepository<Potentia, ReadPotentiaDto, UpdatePotentiaDto> _repository;

    public PotentiaController(IRepository<Potentia, ReadPotentiaDto, UpdatePotentiaDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaPotentia([FromBody] CreatePotentiaDto potentiaDto) 
    {
        var potentia = potentiaDto.MapToPotentia();
        _repository.Create(potentia);
        return CreatedAtAction(nameof(BuscaPotentiaPorId), new {Id = potentia.Id}, potentia.MapToReadPotentiaDto());
    }

    [HttpGet]
    public IActionResult VerPotentias() 
    {
        var listaPotentias = _repository.ReadAll();
        return Ok(listaPotentias);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaPotentiaPorId(int id) 
    {
        var potentia = _repository.ReadById(id);
        if (potentia == null) return NotFound();
        else return Ok(potentia);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPotentia(int id, [FromBody] UpdatePotentiaDto potentiaDto) 
    {
        var potentia = _repository.ReadEntityById(id);
        if (potentia == null) return NotFound();

        _repository.Update(potentiaDto, potentia);

        return Ok(potentiaDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaPotentia (int id) 
    {
        var potentia = _repository.ReadEntityById(id);
        if (potentia == null) return NotFound();

        _repository.Delete(potentia);

        return NoContent();
    }
}