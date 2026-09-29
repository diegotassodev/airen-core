using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VersaoController : ControllerBase {

    private AirenContext _context;
    private IRepository<Versao, ReadVersaoDto> _repository;

    public VersaoController(AirenContext context, IRepository<Versao, ReadVersaoDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaVersao([FromBody] CreateVersaoDto versaoDto) {
        var versao = versaoDto.MapToVersao();
        _context.Versoes!.Add(versao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaVersaoPorId), new {Id = versao.Id}, versao);
    }

    [HttpGet]
    public IEnumerable<ReadVersaoDto> VerVersoes() {
        var listaVersoes = _repository.GetAll();
        return listaVersoes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVersaoPorId(int id) {
        var versao = _repository.GetById(id);
        if (versao == null) return NotFound();
        else return Ok(versao.MapToReadVersao());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVersao(int id, [FromBody] UpdateVersaoDto versaoDto) {
        var versao = _repository.GetById(id);
        if (versao == null) return NotFound();

        versaoDto.MapToVersao(versao);
        _context.SaveChanges();

        return Ok(versaoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVersao (int id) {
        var versao = _repository.GetById(id);
        if (versao == null) return NotFound();

        _context.Versoes!.Remove(versao);
        _context.SaveChanges();

        return NoContent();
    }
}