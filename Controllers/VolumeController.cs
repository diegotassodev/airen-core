using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Profiles;
using AirenApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VolumeController : ControllerBase {

    private AirenContext _context;
    private VolumeService _services;

    public VolumeController(AirenContext context, VolumeService services) {
        _context = context;
        _services = services;
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
        var listaVolumes = _services.GetAll();
        return listaVolumes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVolumePorId(int id) {
        var volume = _services.GetById(id);
        if (volume == null) return NotFound();
        else return Ok(volume.MapToReadVolume());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVolume(int id, [FromBody] UpdateVolumeDto volumeDto) {
        var volume = _services.GetById(id);
        if (volume == null) return NotFound();

        volumeDto.MapToVolume(volume);
        _context.SaveChanges();

        return Ok(volumeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVolume (int id) {
        var volume = _services.GetById(id);
        if (volume == null) return NotFound();

        _context.Volumes!.Remove(volume);
        _context.SaveChanges();

        return NoContent();
    }
}