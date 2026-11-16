using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class VolumeRepository : IRepository<Volume, ReadVolumeDto, UpdateVolumeDto>
{
    private AirenContext _context;

    public VolumeRepository(AirenContext context) 
    {
        _context = context;
    }

    public ReadVolumeDto Create(Volume volume)
    {
        _context.Volumes!.Add(volume);
        _context.SaveChanges();
        return volume.MapToReadVolume();
    }

    public IEnumerable<ReadVolumeDto> ReadAll() 
    {
        return _context.Volumes!
            .Include(v => v.Capitulos)
            .Select(v => v.MapToReadVolume()).ToList();
    }

    public ReadVolumeDto? ReadById(int id) 
    {
        var volume =_context.Volumes!.FirstOrDefault(p => p.Id == id);
        if (volume is not null) return volume.MapToReadVolume();
        else return null;
    }

    public Volume? ReadEntityById(int id) 
    {
        var volume =_context.Volumes!.FirstOrDefault(p => p.Id == id);
        if (volume is not null) return volume;
        else return null;
    }

    public ReadVolumeDto? Update(UpdateVolumeDto volumeDto, Volume volume)
    {
        volumeDto.MapToVolume(volume);
        _context.SaveChanges();
        return volume.MapToReadVolume();
    }

    public void Delete(Volume volume)
    {
        _context.Volumes!.Remove(volume);
        _context.SaveChanges();
    }
}