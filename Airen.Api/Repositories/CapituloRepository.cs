using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class CapituloRepository : IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto>
{
    private AirenContext _context;

    public CapituloRepository(AirenContext context) 
    {
        _context = context;
    }

    public void Create(Capitulo capitulo)
    {
        _context.Capitulos!.Add(capitulo);
        _context.SaveChanges();
    }

    public IEnumerable<ReadCapituloDto> ReadAll() {
        return _context.Capitulos!
            .Include(c => c.Volume)
            .Select(c => c.MapToReadCapitulo()).ToList();
    }

    public Capitulo? ReadById(int id) 
    {
        var capitulo =_context.Capitulos!.FirstOrDefault(c => c.Id == id);
        if (capitulo is not null) return capitulo;
        else return null;
    }

    public void Update(UpdateCapituloDto capituloDto, Capitulo capitulo)
    {
        capituloDto.MapToCapitulo(capitulo);
        _context.SaveChanges();
    }

    public void Delete(Capitulo capitulo)
    {
        _context.Capitulos!.Remove(capitulo);
        _context.SaveChanges();
    }
}
