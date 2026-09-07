using Airen.Api.Data;
using Airen.Api.Data.DTOs;
using Airen.Api.Models;
using Airen.Api.Profiles;
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
