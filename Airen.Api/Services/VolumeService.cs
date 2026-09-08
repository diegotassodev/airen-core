using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class VolumeService {

    private AirenContext _context;

    public VolumeService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadVolumeDto> GetAll() {
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
