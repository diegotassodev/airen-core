using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Repositories;

public class VersaoRepository : IRepository<Versao, ReadVersaoDto> {

    private AirenContext _context;

    public VersaoRepository(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadVersaoDto> GetAll() {
        return _context.Versoes!
            .Include(v => v.Personagem)
            .Include(v => v.Tecnicas)
            .Include(v => v.Hobbies)
            .Include(v => v.Volumes)
            .Select(v => v.MapToReadVersao()).ToList();
    }

    public Versao? GetById(int id) {
        var versao = _context.Versoes!.FirstOrDefault(v => v.Id == id);
        if (versao is not null) return versao;
        else return null;
    }
}
