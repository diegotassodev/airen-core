using Airen.Api.Data;
using Airen.Api.Data.DTOs;
using Airen.Api.Models;
using Airen.Api.Profiles;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class CapituloServices {

    private AirenContext _context;

    public CapituloServices(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadCapituloDto> GetAll() {
        return _context.Capitulos!
            .Include(c => c.Volume)
            .Select(c => c.MapToReadCapitulo()).ToList();
    }

    public Capitulo? GetById(int id) {
        var capitulo =_context.Capitulos!.FirstOrDefault(a => a.Id == id);
        if (capitulo is not null) return capitulo;
        else return null;
    }
}
