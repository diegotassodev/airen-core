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

    public ReadVersaoDto Create(Versao versao)
    {
        _context.Versoes!.Add(versao);
        _context.SaveChanges();
        return versao.MapToReadVersao();
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

    public ReadVersaoDto? ReadById(int id) 
    {
        var versao =_context.Versoes!.FirstOrDefault(p => p.Id == id);
        if (versao is not null) return versao.MapToReadVersao();
        else return null;
    }

    public Versao? ReadEntityById(int id) 
    {
        var versao =_context.Versoes!.FirstOrDefault(p => p.Id == id);
        if (versao is not null) return versao;
        else return null;
    }

    public ReadVersaoDto? Update(UpdateVersaoDto versaoDto, Versao versao)
    {
        var novaVersao = versaoDto.MapToVersao(versao);
        _context.SaveChanges();
        return novaVersao.MapToReadVersao();
    }

    public void Delete(Versao versao)
    {
        _context.Versoes!.Remove(versao);
        _context.SaveChanges();
    }
}