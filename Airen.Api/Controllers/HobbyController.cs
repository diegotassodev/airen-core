using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HobbyController : ControllerBase 
{
    private IRepository<Hobby, ReadHobbyDto, UpdateHobbyDto> _repository;

    public HobbyController(IRepository<Hobby, ReadHobbyDto, UpdateHobbyDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionarHobby([FromBody] CreateHobbyDto hobbyDto) 
    {
        var hobby = hobbyDto.MapToHobby();
        _repository.Create(hobby);
        return CreatedAtAction(nameof(BuscaHobbyPorId), new {Id = hobby.Id}, hobby.MapToReadHobby());
    }

    [HttpGet]
    public IActionResult VerHobbies() 
    {
        var listaHobbies = _repository.ReadAll();
        return Ok(listaHobbies);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHobbyPorId(int id) 
    {
        var hobby = _repository.ReadById(id);
        if (hobby == null) return NotFound();
        else return Ok(hobby);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHobby(int id, [FromBody] UpdateHobbyDto hobbyDto) 
    {
        var hobby = _repository.ReadEntityById(id);
        if (hobby == null) return NotFound();

        _repository.Update(hobbyDto, hobby);

        return Ok(hobbyDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHobby (int id) 
    {
        var hobby = _repository.ReadEntityById(id);
        if (hobby == null) return NotFound();

        _repository.Delete(hobby);

        return NoContent();
    }
}