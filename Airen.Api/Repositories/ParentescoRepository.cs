using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ParentescoRepository : IRepository<Parentesco, ReadParentescoDto, UpdateParentescoDto>
{
    private AirenContext _context;

    public ParentescoRepository(AirenContext context) 
    {
        _context = context;
    }

    public void Create(Parentesco parentesco)
    {
        _context.Parentescos!.Add(parentesco);
        _context.SaveChanges();
    }

    public IEnumerable<ReadParentescoDto> ReadAll() 
    {
        return _context.Parentescos!
            .Include(p => p.Personagem)
            .Include(p => p.Parente)
            .Select(p => p.MapToReadParentesco()).ToList();
    }

    public Parentesco? ReadById(int id) 
    {
        var parentesco =_context.Parentescos!.FirstOrDefault(p => p.Id == id);
        if (parentesco is not null) return parentesco;
        else return null;
    }

    public void Update(UpdateParentescoDto parentescoDto, Parentesco parentesco)
    {
        parentescoDto.MapToParentesco(parentesco);
        _context.SaveChanges();
    }

    public void Delete(Parentesco parentesco)
    {
        _context.Parentescos!.Remove(parentesco);
        _context.SaveChanges();
    }
}
