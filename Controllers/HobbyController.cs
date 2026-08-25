using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Profiles;
using AirenApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HobbyController : ControllerBase {

    private AirenContext _context;
    private HobbyService _service;

    public HobbyController(AirenContext context, HobbyService service) {
        _context = context;
        _service = service;
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
        var listaHobbies = _service.GetAll();
        return listaHobbies;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaHobbyPorId(int id) {
        var hobby = _service.GetById(id);
        if (hobby == null) return NotFound();
        else return Ok(hobby.MapToReadHobby());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaHobby(int id, [FromBody] UpdateHobbyDto hobbyDto) {
        var hobby = _service.GetById(id);
        if (hobby == null) return NotFound();

        hobbyDto.MapToHobby(hobby);
        _context.SaveChanges();

        return Ok(hobbyDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaHobby (int id) {
        var hobby = _service.GetById(id);
        if (hobby == null) return NotFound();

        _context.Hobbies!.Remove(hobby);
        _context.SaveChanges();

        return NoContent();
    }
}