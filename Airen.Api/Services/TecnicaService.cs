using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class TecnicaService {

    private AirenContext _context;

    public TecnicaService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadTecnicaDto> GetAll() {
        return _context.Tecnicas!
            .Include(t => t.Versao)
            .Select(t => t.MapToReadTecnica()).ToList();
    }

    public Tecnica? GetById(int id) {
        var tecnica = _context.Tecnicas!.FirstOrDefault(t => t.Id == id);
        if (tecnica is not null) return tecnica;
        else return null;
    }
}
