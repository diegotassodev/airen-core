using Airen.Api.Data;
using Airen.Api.Data.DTOs;
using Airen.Api.Models;
using Airen.Api.Profiles;
using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AfiliacaoController : ControllerBase {

    private AirenContext _context;
    private AfiliacaoServices _services;

    public AfiliacaoController(AirenContext context, AfiliacaoServices services) {
        _context = context;
        _services = services;
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
        var listaAfiliacoes = _services.GetAll();
        return listaAfiliacoes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaAfiliacaoPorId(int id) {
        var afiliacao = _services.GetById(id);
        if (afiliacao == null) return NotFound();
        else return Ok(afiliacao.MapToReadAfiliacao());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaAfiliacao(int id, [FromBody] UpdateAfiliacaoDto afiliacaoDto) {
        var afiliacao = _services.GetById(id);
        if (afiliacao == null) return NotFound();

        afiliacaoDto.MapToAfiliacao(afiliacao);
        _context.SaveChanges();

        return Ok(afiliacaoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaAfiliacao (int id) {
        var afiliacao = _services.GetById(id);
        if (afiliacao == null) return NotFound();

        _context.Afiliacaos!.Remove(afiliacao);
        _context.SaveChanges();

        return NoContent();
    }
}