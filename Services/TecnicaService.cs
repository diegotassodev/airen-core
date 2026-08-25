using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AirenApi.Services;

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
