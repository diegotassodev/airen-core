using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PotentiaController : ControllerBase {

    private AirenContext _context;
    private PotentiaService _service;

    public PotentiaController(AirenContext context, PotentiaService service) {
        _context = context;
        _service = service;
    }

    [HttpPost]
    public IActionResult AdicionaPotentia([FromBody] CreatePotentiaDto potentiaDto) {
        var potentia = potentiaDto.MapToPotentia();
        _context.Potentias!.Add(potentia);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaPotentiaPorId), new {Id = potentia.Id}, potentia);
    }

    [HttpGet]
    public IEnumerable<ReadPotentiaDto> VerPotentias() {
        var listaPotentias = _service.GetAll();
        return listaPotentias;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaPotentiaPorId(int id) {
        var potentia = _service.GetById(id);
        if (potentia == null) return NotFound();
        else return Ok(potentia.MapToReadPotentiaDto());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPotentia(int id, [FromBody] UpdatePotentiaDto potentiaDto) {
        var potentia = _service.GetById(id);
        if (potentia == null) return NotFound();

        potentiaDto.MapToPotentia(potentia);
        _context.SaveChanges();

        return Ok(potentiaDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaPotentia (int id) {
        var potentia = _service.GetById(id);
        if (potentia == null) return NotFound();

        _context.Potentias!.Remove(potentia);
        _context.SaveChanges();

        return NoContent();
    }
}