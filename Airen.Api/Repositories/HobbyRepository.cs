using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Airen.Api.Repositories;

public class HobbyRepository : IRepository<Hobby, ReadHobbyDto> {

    private AirenContext _context;

    public HobbyRepository(AirenContext context) {
        _context = context;
    }

    public IEnumerable<ReadHobbyDto> GetAll() {
        return _context.Hobbies!
            .Include(h => h.Versao).Include(h => h.Versao!.Personagem)
            .Select(h => h.MapToReadHobby()).ToList();
    }

    public Hobby? GetById(int id) {
        var hobby =_context.Hobbies!.FirstOrDefault(h => h.Id == id);
        if (hobby is not null) return hobby;
        else return null;
    }
}
