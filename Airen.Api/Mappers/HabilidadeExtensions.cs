using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class HabilidadeExtensions 
{
    public static Habilidade MapToHabilidade(this CreateHabilidadeDto dto) 
    {
        return new Habilidade 
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            IdPotentia = dto.IdPotentia
        };
    }

    public static void MapToHabilidade(this UpdateHabilidadeDto dto, Habilidade habilidade) 
    {
        habilidade.Nome = dto.Nome;
        habilidade.Descricao = dto.Descricao;
        habilidade.IdPotentia = dto.IdPotentia;
    }

    public static ReadHabilidadeDto MapToReadHabilidade(this Habilidade obj) 
    {
        return new ReadHabilidadeDto 
        {
            Id = obj.Id,
            Nome = obj.Descricao,
            Descricao = obj.Descricao,
            NomePotentia = obj.Potentia!.Nome
        };
    }
}