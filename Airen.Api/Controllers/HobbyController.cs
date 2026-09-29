using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HobbyController : ControllerBase {

    private AirenContext _context;
    private IRepository<Hobby, ReadHobbyDto> _repository;

    public HobbyController(AirenContext context, IRepository<Hobby, ReadHobbyDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionarHobby([FromBody] CreateHobbyDto hobbyDto) {
        var hobby = hobbyDto.MapToHobby();
        _context.Hobbies!.Add(hobby);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaHobbyPorId), new {Id = hobby.Id}, hobby);
    }

    [HttpGet]
    public IEnumerable<ReadHobbyDto> VerHobbies() {
        var listaHobbies = _repository.GetAll();
        return listaHobbies;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHobbyPorId(int id) {
        var hobby = _repository.GetById(id);
        if (hobby == null) return NotFound();
        else return Ok(hobby.MapToReadHobby());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHobby(int id, [FromBody] UpdateHobbyDto hobbyDto) {
        var hobby = _repository.GetById(id);
        if (hobby == null) return NotFound();

        hobbyDto.MapToHobby(hobby);
        _context.SaveChanges();

        return Ok(hobbyDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHobby (int id) {
        var hobby = _repository.GetById(id);
        if (hobby == null) return NotFound();

        _context.Hobbies!.Remove(hobby);
        _context.SaveChanges();

        return NoContent();
    }
}