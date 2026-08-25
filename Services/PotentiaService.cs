using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AirenApi.Services;

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
