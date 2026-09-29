using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Repositories;

public class HabilidadeRepository : IRepository<Habilidade, ReadHabilidadeDto> {

    private AirenContext _context;

    public HabilidadeRepository(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadHabilidadeDto> GetAll() {
        return _context.Habilidades!
            .Include(h => h.Potentia)
            .Select(h => h.MapToReadHabilidade()).ToList();
    }

    public Habilidade? GetById(int id) {
        var habilidade =_context.Habilidades!.FirstOrDefault(h => h.Id == id);
        if (habilidade is not null) return habilidade;
        else return null;
    }
}
