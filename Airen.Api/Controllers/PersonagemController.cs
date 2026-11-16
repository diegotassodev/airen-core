using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonagemController : ControllerBase 
{
    private IRepository<Personagem, ReadPersonagemDto, UpdatePersonagemDto> _repository;

    public PersonagemController(IRepository<Personagem, ReadPersonagemDto, UpdatePersonagemDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaPersonagem([FromBody] CreatePersonagemDto personagemDto) 
    {
        var personagem = personagemDto.MapToPersonagem();
        _repository.Create(personagem);
        return CreatedAtAction(nameof(BuscaPersonagemPorId), new {Id = personagem.Id}, personagem.MapToReadPersonagem());
    }

    [HttpGet]
    public IActionResult VerPersonagens() 
    {
        var listaPersonagens = _repository.ReadAll();
        return Ok(listaPersonagens);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaPersonagemPorId(int id) 
    {
        var personagem = _repository.ReadById(id);
        if (personagem == null) return NotFound();
        else return Ok(personagem);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPersonagem(int id, [FromBody] UpdatePersonagemDto personagemDto) 
    {
        var personagem = _repository.ReadEntityById(id);
        if (personagem == null) return NotFound();

        _repository.Update(personagemDto, personagem);

        return Ok(personagemDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaPersonagem (int id) 
    {
        var personagem = _repository.ReadEntityById(id);
        if (personagem == null) return NotFound();

        _repository.Delete(personagem);

        return NoContent();
    }
}