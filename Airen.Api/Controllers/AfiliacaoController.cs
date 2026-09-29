using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AfiliacaoController : ControllerBase {

    private AirenContext _context;
    private IRepository<Afiliacao, ReadAfiliacaoDto> _repository;

    public AfiliacaoController(AirenContext context, IRepository<Afiliacao, ReadAfiliacaoDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaAfiliacao([FromBody] CreateAfiliacaoDto afiliacaoDto) {
        var afiliacao = afiliacaoDto.MapToAfiliacao();
        _context.Afiliacaos!.Add(afiliacao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaAfiliacaoPorId), new {Id = afiliacao.Id}, afiliacao);
    }

    [HttpGet]
    public IEnumerable<ReadAfiliacaoDto> VerAfiliacoes() {
        var listaAfiliacoes = _repository.GetAll();
        return listaAfiliacoes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaAfiliacaoPorId(int id) {
        var afiliacao = _repository.GetById(id);
        if (afiliacao == null) return NotFound();
        else return Ok(afiliacao.MapToReadAfiliacao());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaAfiliacao(int id, [FromBody] UpdateAfiliacaoDto afiliacaoDto) {
        var afiliacao = _repository.GetById(id);
        if (afiliacao == null) return NotFound();

        afiliacaoDto.MapToAfiliacao(afiliacao);
        _context.SaveChanges();

        return Ok(afiliacaoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaAfiliacao (int id) {
        var afiliacao = _repository.GetById(id);
        if (afiliacao == null) return NotFound();

        _context.Afiliacaos!.Remove(afiliacao);
        _context.SaveChanges();

        return NoContent();
    }
}