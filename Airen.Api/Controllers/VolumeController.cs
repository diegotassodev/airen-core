using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Airen.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolumeController : ControllerBase 
{
    private IRepository<Volume, ReadVolumeDto, UpdateVolumeDto> _repository;

    public VolumeController(IRepository<Volume, ReadVolumeDto, UpdateVolumeDto> repository) 
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult AdicionaVolume([FromBody] CreateVolumeDto volumeDto) 
    {
        var volume = volumeDto.MapToVolume();
        _repository.Create(volume);
        return CreatedAtAction(nameof(BuscaVolumePorId), new {Id = volume.Id}, volume);
    }

    [HttpGet]
    public IEnumerable<ReadVolumeDto> VerVolumes() 
    {
        var listaVolumes = _repository.ReadAll();
        return listaVolumes;
    }

    [HttpGet("{id}")]
    public IActionResult BuscaVolumePorId(int id)
    {
        var volume = _repository.ReadById(id);
        if (volume == null) return NotFound();
        else return Ok(volume.MapToReadVolume());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVolume(int id, [FromBody] UpdateVolumeDto volumeDto) 
    {
        var volume = _repository.ReadById(id);
        if (volume == null) return NotFound();

        _repository.Update(volumeDto, volume);

        return Ok(volumeDto);
    } 

    [HttpDelete("{id}")]
    public IActionResult DeletaVolume (int id) 
    {
        var volume = _repository.ReadById(id);
        if (volume == null) return NotFound();

        _repository.Delete(volume);

        return NoContent();
    }
}