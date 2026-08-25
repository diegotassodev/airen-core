using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AirenApi.Services;

public class CapituloServices {

    private AirenContext _context;

    public CapituloServices(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadCapituloDto> GetAll() {
        return _context.Capitulos!
            .Include(c => c.Volume)
            .Select(c => c.MapToReadCapitulo()).ToList();
    }

    public Capitulo? GetById(int id) {
        var capitulo =_context.Capitulos!.FirstOrDefault(a => a.Id == id);
        if (capitulo is not null) return capitulo;
        else return null;
    }
}
