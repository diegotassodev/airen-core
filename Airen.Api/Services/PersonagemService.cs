using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Api.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Services;

public class PersonagemService {

    private AirenContext _context;

    public PersonagemService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadPersonagemDto> GetAll() {
        return _context.Personagens!
            .Include(p => p.Potentia)
            .Include(p => p.Potentia!).ThenInclude(p => p.Habilidades)
            .Include(p => p.Versoes!).ThenInclude(v => v.Hobbies)
            .Include(p => p.Versoes!).ThenInclude(v => v.Tecnicas)
            .Include(p => p.Versoes!).ThenInclude(v => v.Volumes)
            .Include(p => p.Afiliacoes)
            .Include(p => p.Parentes!).ThenInclude(p => p.Parente)
            .Select(p => p.MapToReadPersonagem()).ToList();
    }

    public Personagem? GetById(int id) {
        var personagem = _context.Personagens!.FirstOrDefault(p => p.Id == id);
        if (personagem is not null) return personagem;
        else return null;
    }
}
