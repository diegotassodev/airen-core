using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Api.DTOs;
using Airen.Api.Mappers;

namespace Airen.Api.Services;

public class AfiliacaoServices {

    private AirenContext _context;

    public AfiliacaoServices(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadAfiliacaoDto> GetAll() {
        return _context.Afiliacaos!.Select(a => a.MapToReadAfiliacao()).ToList();
    }

    public Afiliacao? GetById(int id) {
        var afiliacao =_context.Afiliacaos!.FirstOrDefault(a => a.Id == id);
        if (afiliacao is not null) return afiliacao;
        else return null;
    }
}
