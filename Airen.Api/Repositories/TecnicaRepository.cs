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

    public void Create(Tecnica tecnica)
    {
        _context.Tecnicas!.Add(tecnica);
        _context.SaveChanges();
    }

    public IEnumerable<ReadTecnicaDto> ReadAll() 
    {
        return _context.Tecnicas!
            .Include(t => t.Versao)
            .Select(t => t.MapToReadTecnica()).ToList();
    }

    public Tecnica? ReadById(int id) 
    {
        var tecnica =_context.Tecnicas!.FirstOrDefault(p => p.Id == id);
        if (tecnica is not null) return tecnica;
        else return null;
    }

    public void Update(UpdateTecnicaDto tecnicaDto, Tecnica tecnica)
    {
        tecnicaDto.MapToTecnica(tecnica);
        _context.SaveChanges();
    }

    public void Delete(Tecnica tecnica)
    {
        _context.Tecnicas!.Remove(tecnica);
        _context.SaveChanges();
    }
}