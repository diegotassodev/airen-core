using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonagemController : ControllerBase {

    private AirenContext _context;
    private IRepository<Personagem, ReadPersonagemDto> _repository;

    public PersonagemController(AirenContext context, IRepository<Personagem, ReadPersonagemDto> repository) {
        _context = context;
        _repository = repository;
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
        var listaPersonagens = _repository.GetAll();
        return listaPersonagens;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaPersonagemPorId(int id) {
        var personagem = _repository.GetById(id);
        if (personagem == null) return NotFound();
        else return Ok(personagem.MapToReadPersonagem());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPersonagem(int id, [FromBody] UpdatePersonagemDto personagemDto) {
        var personagem = _repository.GetById(id);
        if (personagem == null) return NotFound();

        personagemDto.MapToPersonagem(personagem);
        _context.SaveChanges();

        return Ok(personagemDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaPersonagem (int id) {
        var personagem = _repository.GetById(id);
        if (personagem == null) return NotFound();

        _context.Personagens!.Remove(personagem);
        _context.SaveChanges();

        return NoContent();
    }
}