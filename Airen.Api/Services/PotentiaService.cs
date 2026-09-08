using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Api.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class PotentiaService {

    private AirenContext _context;

    public PotentiaService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadPotentiaDto> GetAll() {
        return _context.Potentias!
            .Include(p => p.Habilidades)
            .Include(p => p.Personagem)
            .Select(p => p.MapToReadPotentiaDto()).ToList();
    }

    public Potentia? GetById(int id) {
        var potentia = _context.Potentias!.FirstOrDefault(p => p.Id == id);
        if (potentia is not null) return potentia;
        else return null;
    }
}
