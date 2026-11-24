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

    public ReadCapituloDto? Create(Capitulo capitulo)
    {
        if (capitulo.IdVolume is not int idVolume) return null;
        var volume = _context.Volumes!.Find(idVolume);
        if (volume is null) return null;

        capitulo.Volume = volume;
        _context.Capitulos!.Add(capitulo);
        _context.SaveChanges();
        return capitulo.MapToReadCapitulo();
    }

    public IEnumerable<ReadCapituloDto> ReadAll() {
        return _context.Capitulos!
            .Include(c => c.Volume)
            .Select(c => c.MapToReadCapitulo()).ToList();
    }

    public ReadCapituloDto? ReadById(int id) 
    {
        var capitulo = _context.Capitulos!
            .Include(c => c.Volume)
            .FirstOrDefault(c => c.Id == id);
        if (capitulo is not null) return capitulo.MapToReadCapitulo();
        else return null;
    }

    public Capitulo? ReadEntityById(int id) 
    {
        var capitulo = _context.Capitulos!
            .Include(c => c.Volume)
            .FirstOrDefault(c => c.Id == id);
        if (capitulo is not null) return capitulo;
        else return null;
    }

    public ReadCapituloDto? Update(UpdateCapituloDto capituloDto, Capitulo capitulo)
    {
        var novoCapitulo = capituloDto.MapToCapitulo(capitulo);
        _context.SaveChanges();
        return novoCapitulo.MapToReadCapitulo();
    }

    public void Delete(Capitulo capitulo)
    {
        _context.Capitulos!.Remove(capitulo);
        _context.SaveChanges();
    }
}
