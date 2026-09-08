using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class ParentescoService {

    private AirenContext _context;

    public ParentescoService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadParentescoDto> GetAll() {
        return _context.Parentescos!
            .Include(p => p.Personagem)
            .Include(p => p.Parente)
            .Select(p => p.MapToReadParentesco()).ToList();
    }

    public Parentesco? GetById(int id) {
        var parentesco =_context.Parentescos!.FirstOrDefault(p => p.Id == id);
        if (parentesco is not null) return parentesco;
        else return null;
    }
}
