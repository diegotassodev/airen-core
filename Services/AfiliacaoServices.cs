using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;

namespace AirenApi.Services;

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
