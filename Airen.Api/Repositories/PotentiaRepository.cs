using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class PotentiaRepository : IRepository<Potentia, ReadPotentiaDto, UpdatePotentiaDto>
{
    private AirenContext _context;

    public PotentiaRepository(AirenContext context) 
    {
        _context = context;
    }

    public void Create(Potentia potentia)
    {
        _context.Potentias!.Add(potentia);
        _context.SaveChanges();
    }

    public IEnumerable<ReadPotentiaDto> ReadAll() 
    {
        return _context.Potentias!
            .Include(p => p.Habilidades)
            .Include(p => p.Personagem)
            .Select(p => p.MapToReadPotentiaDto()).ToList();
    }

    public Potentia? ReadById(int id) 
    {
        var potentia =_context.Potentias!.FirstOrDefault(p => p.Id == id);
        if (potentia is not null) return potentia;
        else return null;
    }

    public void Update(UpdatePotentiaDto potentiaDto, Potentia potentia)
    {
        potentiaDto.MapToPotentia(potentia);
        _context.SaveChanges();
    }

    public void Delete(Potentia potentia)
    {
        _context.Potentias!.Remove(potentia);
        _context.SaveChanges();
    }
}