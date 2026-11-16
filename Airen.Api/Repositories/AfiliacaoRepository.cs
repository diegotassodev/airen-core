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

    public ReadAfiliacaoDto Create(Afiliacao afiliacao)
    {
        _context.Afiliacaos!.Add(afiliacao);
        _context.SaveChanges();
        return afiliacao.MapToReadAfiliacao();
    }

    public IEnumerable<ReadAfiliacaoDto> ReadAll() 
    {
        return _context.Afiliacaos!.Select(a => a.MapToReadAfiliacao()).ToList();
    }

    public ReadAfiliacaoDto? ReadById(int id) 
    {
        var afiliacao =_context.Afiliacaos!.FirstOrDefault(a => a.Id == id);
        if (afiliacao is not null) return afiliacao.MapToReadAfiliacao();
        else return null;
    }

    public Afiliacao? ReadEntityById(int id) 
    {
        var afiliacao =_context.Afiliacaos!.FirstOrDefault(a => a.Id == id);
        if (afiliacao is not null) return afiliacao;
        else return null;
    }

    public ReadAfiliacaoDto? Update(UpdateAfiliacaoDto afiliacaoDto, Afiliacao afiliacao)
    {
        var novaAfiliacao = afiliacaoDto.MapToAfiliacao(afiliacao);
        _context.SaveChanges();
        return novaAfiliacao.MapToReadAfiliacao();
    }

    public void Delete(Afiliacao afiliacao)
    {
        _context.Afiliacaos!.Remove(afiliacao);
        _context.SaveChanges();
    }
}
