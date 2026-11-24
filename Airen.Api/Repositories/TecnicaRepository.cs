using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class TecnicaRepository : IRepository<Tecnica, ReadTecnicaDto, UpdateTecnicaDto>
{
    private AirenContext _context;

    public TecnicaRepository(AirenContext context) 
    {
        _context = context;
    }

    public ReadTecnicaDto? Create(Tecnica tecnica)
    {
        var versao = _context.Versoes!
            .Include(item => item.Personagem)
            .FirstOrDefault(item => item.Id == tecnica.IdVersao);
        if (versao is null)
            return null;

        tecnica.Versao = versao;
        _context.Tecnicas!.Add(tecnica);
        _context.SaveChanges();
        return tecnica.MapToReadTecnica();
    }

    public IEnumerable<ReadTecnicaDto> ReadAll() 
    {
        return _context.Tecnicas!
            .Include(t => t.Versao).ThenInclude(v => v!.Personagem)
            .Select(t => t.MapToReadTecnica()).ToList();
    }

    public ReadTecnicaDto? ReadById(int id) 
    {
        var tecnica =_context.Tecnicas!.FirstOrDefault(p => p.Id == id);
        if (tecnica is not null) return tecnica.MapToReadTecnica();
        else return null;
    }

    public Tecnica? ReadEntityById(int id) 
    {
        var tecnica =_context.Tecnicas!.FirstOrDefault(p => p.Id == id);
        if (tecnica is not null) return tecnica;
        else return null;
    }

    public ReadTecnicaDto? Update(UpdateTecnicaDto tecnicaDto, Tecnica tecnica)
    {
        var novaTecnica = tecnicaDto.MapToTecnica(tecnica);
        _context.SaveChanges();
        return novaTecnica.MapToReadTecnica();
    }

    public void Delete(Tecnica tecnica)
    {
        _context.Tecnicas!.Remove(tecnica);
        _context.SaveChanges();
    }
}