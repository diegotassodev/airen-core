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

    public ReadPotentiaDto? Create(Potentia potentia)
    {
        var personagem = _context.Personagens!.Find(potentia.PersonagemId);
        if (personagem is null)
            return null;

        potentia.Personagem = personagem;
        _context.Potentias!.Add(potentia);
        _context.SaveChanges();
        return potentia.MapToReadPotentiaDto();
    }

    public IEnumerable<ReadPotentiaDto> ReadAll() 
    {
        return _context.Potentias!
            .Include(p => p.Habilidades)
            .Include(p => p.Personagem)
            .Select(p => p.MapToReadPotentiaDto()).ToList();
    }

    public ReadPotentiaDto? ReadById(int id) 
    {
        var potentia =_context.Potentias!.FirstOrDefault(p => p.Id == id);
        if (potentia is not null) return potentia.MapToReadPotentiaDto();
        else return null;
    }

    public Potentia? ReadEntityById(int id) 
    {
        var potentia =_context.Potentias!.FirstOrDefault(p => p.Id == id);
        if (potentia is not null) return potentia;
        else return null;
    }

    public ReadPotentiaDto? Update(UpdatePotentiaDto potentiaDto, Potentia potentia)
    {
        var novaPotentia = potentiaDto.MapToPotentia(potentia);
        _context.SaveChanges();
        return novaPotentia.MapToReadPotentiaDto();
    }

    public void Delete(Potentia potentia)
    {
        _context.Potentias!.Remove(potentia);
        _context.SaveChanges();
    }
}