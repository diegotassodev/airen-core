using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AirenApi.Services;

public class HabilidadeServices {

    private AirenContext _context;

    public HabilidadeServices(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadHabilidadeDto> GetAll() {
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
