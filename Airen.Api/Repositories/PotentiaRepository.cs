using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Repositories;

public class PotentiaRepository : IRepository<Potentia, ReadPotentiaDto> {

    private AirenContext _context;

    public PotentiaRepository(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadPotentiaDto> GetAll() {
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
