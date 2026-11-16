using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class HabilidadeRepository : IRepository<Habilidade, ReadHabilidadeDto, UpdateHabilidadeDto>
{
    private AirenContext _context;

    public HabilidadeRepository(AirenContext context) 
    {
        _context = context;
    }

    public ReadHabilidadeDto Create(Habilidade habilidade)
    {
        _context.Habilidades!.Add(habilidade);
        _context.SaveChanges();
        return habilidade.MapToReadHabilidade();
    }

    public IEnumerable<ReadHabilidadeDto> ReadAll() 
    {
        return _context.Habilidades!
            .Include(h => h.Potentia)
            .Select(h => h.MapToReadHabilidade()).ToList();
    }

    public ReadHabilidadeDto? ReadById(int id) 
    {
        var habilidade =_context.Habilidades!.FirstOrDefault(h => h.Id == id);
        if (habilidade is not null) return habilidade.MapToReadHabilidade();
        else return null;
    }

    public Habilidade? ReadEntityById(int id) 
    {
        var habilidade =_context.Habilidades!.FirstOrDefault(h => h.Id == id);
        if (habilidade is not null) return habilidade;
        else return null;
    }

    public ReadHabilidadeDto? Update(UpdateHabilidadeDto habilidadeDto, Habilidade habilidade)
    {
        var novaHabilidade = habilidadeDto.MapToHabilidade(habilidade);
        _context.SaveChanges();
        return novaHabilidade.MapToReadHabilidade();
    }

    public void Delete(Habilidade habilidade)
    {
        _context.Habilidades!.Remove(habilidade);
        _context.SaveChanges();
    }
}
