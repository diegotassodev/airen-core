using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Repositories;

public class VolumeRepository : IRepository<Volume, ReadVolumeDto>{

    private AirenContext _context;

    public VolumeRepository(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadVolumeDto> GetAll() {
        return _context.Volumes!
            .Include(v => v.Capitulos)
            .Select(v => v.MapToReadVolume()).ToList();
    }

    public Volume? GetById(int id) {
        var volume = _context.Volumes!
            .Include(v => v.Capitulos)
            .FirstOrDefault(v => v.Id == id);
        if (volume is not null) return volume;
        else return null;
    }
}
