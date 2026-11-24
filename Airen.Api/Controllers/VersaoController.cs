using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Airen.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VersaoController : ControllerBase 
{
    private IRepository<Versao, ReadVersaoDto, UpdateVersaoDto> _repository;
    private readonly AirenContext? _context;

    public VersaoController(
        IRepository<Versao, ReadVersaoDto, UpdateVersaoDto> repository,
        AirenContext? context = null)
    {
        _repository = repository;
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaVersao([FromBody] CreateVersaoDto versaoDto) 
    {
        var versao = versaoDto.MapToVersao();
        if (versaoDto.IdVolumes.Count > 0)
        {
            var idsVolumes = versaoDto.IdVolumes.Distinct().ToList();
            var volumes = _context?.Volumes?
                .Where(volume => idsVolumes.Contains(volume.Id))
                .ToList();
            if (volumes is null || volumes.Count != idsVolumes.Count) return BadRequest("Um ou mais volumes informados não existem.");

            versao.Volumes = volumes;
        }

        _repository.Create(versao);
        return CreatedAtAction(nameof(BuscaVersaoPorId), new {Id = versao.Id}, versao.MapToReadVersao());
    }

    [HttpGet]
    public IActionResult VerVersoes() 
    {
        var listaVersoes = _repository.ReadAll();
        return Ok(listaVersoes);
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVersaoPorId(int id) 
    {
        var versao = _repository.ReadById(id);
        if (versao == null) return NotFound();
        else return Ok(versao);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVersao(int id, [FromBody] UpdateVersaoDto versaoDto) 
    {
        var versao = _repository.ReadEntityById(id);
        if (versao == null) return NotFound();

        _repository.Update(versaoDto, versao);

        return Ok(versaoDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVersao (int id) 
    {
        var versao = _repository.ReadEntityById(id);
        if (versao == null) return NotFound();

        _repository.Delete(versao);

        return NoContent();
    }
}