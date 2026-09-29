using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class VersaoRepository : IRepository<Versao, ReadVersaoDto, UpdateVersaoDto>
{
    private AirenContext _context;

    public VersaoRepository(AirenContext context) 
    {
        _context = context;
    }

    public void Create(Versao versao)
    {
        _context.Versoes!.Add(versao);
        _context.SaveChanges();
    }

    public IEnumerable<ReadVersaoDto> ReadAll() 
    {
        return _context.Versoes!
            .Include(v => v.Personagem)
            .Include(v => v.Tecnicas)
            .Include(v => v.Hobbies)
            .Include(v => v.Volumes)
            .Select(v => v.MapToReadVersao()).ToList();
    }

    public Versao? ReadById(int id) 
    {
        var versao =_context.Versoes!.FirstOrDefault(p => p.Id == id);
        if (versao is not null) return versao;
        else return null;
    }

    public void Update(UpdateVersaoDto versaoDto, Versao versao)
    {
        versaoDto.MapToVersao(versao);
        _context.SaveChanges();
    }

    public void Delete(Versao versao)
    {
        _context.Versoes!.Remove(versao);
        _context.SaveChanges();
    }
}