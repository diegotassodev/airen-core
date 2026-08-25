using AirenApi.Data;
using AirenApi.Data.DTOs;
using AirenApi.Models;
using AirenApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AirenApi.Services;

public class VersaoService {

    private AirenContext _context;

    public VersaoService(AirenContext context) {
        _context = context;
    }

    public ICollection<ReadVersaoDto> GetAll() {
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
