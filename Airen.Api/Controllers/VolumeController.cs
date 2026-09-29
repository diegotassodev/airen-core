using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolumeController : ControllerBase {

    private AirenContext _context;
    private IRepository<Volume, ReadVolumeDto> _repository;

    public VolumeController(AirenContext context, IRepository<Volume, ReadVolumeDto> repository) {
        _context = context;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaVolume([FromBody] CreateVolumeDto volumeDto) {
        var volume = volumeDto.MapToVolume();
        _context.Volumes!.Add(volume);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaVolumePorId), new {Id = volume.Id}, volume);
    }

    [HttpGet]
    public IEnumerable<ReadVolumeDto> VerVolumes() {
        var listaVolumes = _repository.GetAll();
        return listaVolumes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVolumePorId(int id) {
        var volume = _repository.GetById(id);
        if (volume == null) return NotFound();
        else return Ok(volume.MapToReadVolume());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVolume(int id, [FromBody] UpdateVolumeDto volumeDto) {
        var volume = _repository.GetById(id);
        if (volume == null) return NotFound();

        volumeDto.MapToVolume(volume);
        _context.SaveChanges();

        return Ok(volumeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVolume (int id) {
        var volume = _repository.GetById(id);
        if (volume == null) return NotFound();

        _context.Volumes!.Remove(volume);
        _context.SaveChanges();

        return NoContent();
    }
}