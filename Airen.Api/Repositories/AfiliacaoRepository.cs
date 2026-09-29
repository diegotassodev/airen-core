using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;

using Airen.Contracts.DTOs;
using Airen.Api.Mappers;

namespace Airen.Api.Repositories;

public class AfiliacaoRepository : IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto>
{
    private AirenContext _context;

    public AfiliacaoRepository(AirenContext context) 
    {
        _context = context;
    }

    public void Create(Afiliacao afiliacao)
    {
        _context.Afiliacaos!.Add(afiliacao);
        _context.SaveChanges();
    }

    public IEnumerable<ReadAfiliacaoDto> ReadAll() 
    {
        return _context.Afiliacaos!.Select(a => a.MapToReadAfiliacao()).ToList();
    }

    public Afiliacao? ReadById(int id) 
    {
        var afiliacao =_context.Afiliacaos!.FirstOrDefault(a => a.Id == id);
        if (afiliacao is not null) return afiliacao;
        else return null;
    }

    public void Update(UpdateAfiliacaoDto afiliacaoDto, Afiliacao afiliacao)
    {
        afiliacaoDto.MapToAfiliacao(afiliacao);
        _context.SaveChanges();
    }

    public void Delete(Afiliacao afiliacao)
    {
        _context.Afiliacaos!.Remove(afiliacao);
        _context.SaveChanges();
    }
}
