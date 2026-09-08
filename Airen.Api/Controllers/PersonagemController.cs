using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Api.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonagemController : ControllerBase {

    private AirenContext _context;
    private PersonagemService _service;

    public PersonagemController(AirenContext context, PersonagemService service) {
        _context = context;
        _service = service;
    }

    [HttpPost]
    public IActionResult AdicionaPersonagem([FromBody] CreatePersonagemDto personagemDto) {
        var personagem = personagemDto.MapToPersonagem();
        _context.Personagens!.Add(personagem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaPersonagemPorId), new {Id = personagem.Id}, personagem);
    }

    [HttpGet]
    public IEnumerable<ReadPersonagemDto> VerPersonagens() {
        var listaPersonagens = _service.GetAll();
        return listaPersonagens;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaPersonagemPorId(int id) {
        var personagem = _service.GetById(id);
        if (personagem == null) return NotFound();
        else return Ok(personagem.MapToReadPersonagem());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPersonagem(int id, [FromBody] UpdatePersonagemDto personagemDto) {
        var personagem = _service.GetById(id);
        if (personagem == null) return NotFound();

        personagemDto.MapToPersonagem(personagem);
        _context.SaveChanges();

        return Ok(personagemDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaPersonagem (int id) {
        var personagem = _service.GetById(id);
        if (personagem == null) return NotFound();

        _context.Personagens!.Remove(personagem);
        _context.SaveChanges();

        return NoContent();
    }
}