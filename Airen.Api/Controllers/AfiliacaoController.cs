using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AfiliacaoController : ControllerBase 
{
    private IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto> _repository;

    public AfiliacaoController(IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaAfiliacao([FromBody] CreateAfiliacaoDto afiliacaoDto) 
    {
        var afiliacao = afiliacaoDto.MapToAfiliacao();

        _repository.Create(afiliacao);

        return CreatedAtAction(nameof(BuscaAfiliacaoPorId), new {Id = afiliacao.Id}, afiliacao.MapToReadAfiliacao());
    }

    [HttpGet]
    public IActionResult VerAfiliacoes() 
    {
        var listaAfiliacoes = _repository.ReadAll();
        return Ok(listaAfiliacoes);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaAfiliacaoPorId(int id) 
    {
        var afiliacao = _repository.ReadById(id);
        if (afiliacao == null) return NotFound();
        else return Ok(afiliacao);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaAfiliacao(int id, [FromBody] UpdateAfiliacaoDto afiliacaoDto) 
    {
        var afiliacao = _repository.ReadEntityById(id);
        if (afiliacao == null) return NotFound();

        _repository.Update(afiliacaoDto, afiliacao);

        return Ok(afiliacaoDto.MapToAfiliacao().MapToReadAfiliacao());
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaAfiliacao (int id) {
        var afiliacao = _repository.ReadEntityById(id);
        if (afiliacao == null) return NotFound();

        _repository.Delete(afiliacao);

        return NoContent();
    }
}