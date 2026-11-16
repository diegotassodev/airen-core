using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VersaoController : ControllerBase 
{
    private IRepository<Versao, ReadVersaoDto, UpdateVersaoDto> _repository;

    public VersaoController(IRepository<Versao, ReadVersaoDto, UpdateVersaoDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaVersao([FromBody] CreateVersaoDto versaoDto) 
    {
        var versao = versaoDto.MapToVersao();
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