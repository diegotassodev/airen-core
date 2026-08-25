using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Profiles;
using AirenApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VersaoController : ControllerBase {

    private AirenContext _context;
    private VersaoService _service;

    public VersaoController(AirenContext context, VersaoService service) {
        _context = context;
        _service = service;
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
        var listaVersoes = _service.GetAll();
        return listaVersoes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVersaoPorId(int id) {
        var versao = _service.GetById(id);
        if (versao == null) return NotFound();
        else return Ok(versao.MapToReadVersao());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVersao(int id, [FromBody] UpdateVersaoDto versaoDto) {
        var versao = _service.GetById(id);
        if (versao == null) return NotFound();

        versaoDto.MapToVersao(versao);
        _context.SaveChanges();

        return Ok(versaoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVersao (int id) {
        var versao = _service.GetById(id);
        if (versao == null) return NotFound();

        _context.Versoes!.Remove(versao);
        _context.SaveChanges();

        return NoContent();
    }
}